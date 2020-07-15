using CarsIsland.EventBus.Events;
using CarsIsland.EventBus.Events.Interfaces;
using CarsIsland.EventBus.Services.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CarsIsland.EventBus
{
    public class AzureServiceBusEventBus : IEventBus
    {
        private readonly SubscriptionClient _subscriptionClient;
        private readonly IEventBusSubscriptionsManager _subscriptionManager;
        private readonly IServiceBusConnectionManagementService _serviceBusConnectionManagementService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AzureServiceBusEventBus> _logger;

        public AzureServiceBusEventBus(IServiceBusConnectionManagementService serviceBusConnectionManagementService,
                        IEventBusSubscriptionsManager subscriptionManager,
                        IServiceProvider serviceProvider,
                        ILogger<AzureServiceBusEventBus> logger,
                        string subscriptionClientName)
        {
            _serviceBusConnectionManagementService = serviceBusConnectionManagementService;
            _subscriptionManager = subscriptionManager;
            _subscriptionClient = _subscriptionClient = new SubscriptionClient(_serviceBusConnectionManagementService.ServiceBusConnectionStringBuilder,
                subscriptionClientName);
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task SetupAsync()
        {
            try
            {
                await RemoveDefaultRuleAsync();
                RegisterSubscriptionClientMessageHandler();
            }

            catch (MessagingEntityNotFoundException)
            {
                _logger.LogWarning("The messaging entity '{DefaultRuleName}' Could not be found.", RuleDescription.DefaultRuleName);
            }
        }

        public async Task PublishAsync(IntegrationEvent @event)
        {
            var eventName = @event.GetType().Name;
            var jsonMessage = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            var message = new Message
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = body,
                Label = eventName,
            };

            var topicClient = _serviceBusConnectionManagementService.CreateTopicClient();
            await topicClient.SendAsync(message);
        }

        public async Task SubscribeAsync<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;

            var containsKey = _subscriptionManager.HasSubscriptionsForEvent<T>();
            if (!containsKey)
            {
                try
                {
                    await _subscriptionClient.AddRuleAsync(new RuleDescription
                    {
                        Filter = new CorrelationFilter { Label = eventName },
                        Name = eventName
                    });
                }
                catch (ServiceBusException)
                {
                    _logger.LogWarning("The messaging entity '{eventName}' already exists.", eventName);
                }
            }

            _logger.LogInformation("Subscribing to event '{EventName}' with '{EventHandler}'", eventName, typeof(TH).Name);
            _subscriptionManager.AddSubscription<T, TH>();
        }

        public async Task UnsubscribeAsync<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;

            try
            {
                await _subscriptionClient.RemoveRuleAsync(eventName);
            }
            catch (MessagingEntityNotFoundException)
            {
                _logger.LogWarning("The messaging entity '{eventName}' could not be found.", eventName);
            }

            _logger.LogInformation("Unsubscribing from event '{EventName}'", eventName);
            _subscriptionManager.RemoveSubscription<T, TH>();
        }

        private void RegisterSubscriptionClientMessageHandler()
        {
            _subscriptionClient.RegisterMessageHandler(
                async (message, token) =>
                {
                    var eventName = message.Label;
                    var messageData = Encoding.UTF8.GetString(message.Body);

                    if (await ProcessEvent(eventName, messageData))
                    {
                        await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                    }
                },
                new MessageHandlerOptions(ExceptionReceivedHandler) { MaxConcurrentCalls = 10, AutoComplete = false });
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            var ex = exceptionReceivedEventArgs.Exception;
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            _logger.LogError(ex, "ERROR handling message: '{ExceptionMessage}' - Context: '{ExceptionContext}'", ex.Message, context);

            return Task.CompletedTask;
        }

        private async Task<bool> ProcessEvent(string eventName, string message)
        {
            var processed = false;
            if (_subscriptionManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = _subscriptionManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions)
                {
                    var handler = _serviceProvider.GetRequiredService(subscription.HandlerType);
                    if (handler == null) continue;

                    var eventType = _subscriptionManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                    await (Task)concreteType.GetMethod("HandleAsync").Invoke(handler, new object[] { integrationEvent });
                    processed = true;
                }
            }

            return processed;
        }

        private async Task RemoveDefaultRuleAsync()
        {
            try
            {
                await _subscriptionClient.RemoveRuleAsync(RuleDescription.DefaultRuleName);
            }
            catch (MessagingEntityNotFoundException)
            {
                _logger.LogWarning("The messaging entity '{DefaultRuleName}' Could not be found.", RuleDescription.DefaultRuleName);
            }
        }
    }
}
