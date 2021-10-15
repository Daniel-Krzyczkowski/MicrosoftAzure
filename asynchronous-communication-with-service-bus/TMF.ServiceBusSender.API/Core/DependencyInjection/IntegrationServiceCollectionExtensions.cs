using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TMF.ServiceBusReceiver.Common;

namespace TMF.ServiceBusSender.API.Core.DependencyInjection
{
    internal static class IntegrationServiceCollectionExtensions
    {
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services)
        {
            var eventBusConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<EventBusConfiguration>>().Value;
            services.AddSingleton<EventBusConfiguration>(eventBusConfiguration);

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            services.AddSingleton(implementationFactory =>
            {
                //Connection string value in app settings has to be properly added (for either queue or topic with subscriptions):
                var serviceBusClient = new ServiceBusClient(eventBusConfiguration.ListenAndSendConnectionString);
                return serviceBusClient;
            });

            services.AddSingleton(implementationFactory =>
            {
                var serviceBusClient = implementationFactory.GetRequiredService<ServiceBusClient>();
                // Creates sender for specific topic:
                //var serviceBusSender = serviceBusClient.CreateSender(eventBusConfiguration.TopicName);

                //Creates sender for specific queue:
                var serviceBusSender = serviceBusClient.CreateSender(eventBusConfiguration.QueueName);

                return serviceBusSender;
            });

            services.AddSingleton(implementationFactory =>
            {
                //Connection string value in app settings has to be properly added:
                var serviceBusAdministrationClient = new ServiceBusAdministrationClient(eventBusConfiguration
                                                                                        .ListenAndSendConnectionString);
                return serviceBusAdministrationClient;
            });

            services.AddSingleton(implementationFactory =>
            {
                var serviceBusClient = implementationFactory.GetRequiredService<ServiceBusClient>();

                // Creates receiver for specific topic:
                //var serviceBusReceiver = serviceBusClient.CreateProcessor(eventBusConfiguration.TopicName,
                //                                                          eventBusConfiguration.Subscription,
                //                                                          new ServiceBusProcessorOptions
                //                                                          {
                //                                                              AutoCompleteMessages = false
                //                                                          });

                // Creates receiver for specific queue:
                var serviceBusReceiver = serviceBusClient.CreateProcessor(eventBusConfiguration.QueueName,
                                                                          new ServiceBusProcessorOptions
                                                                          {
                                                                              AutoCompleteMessages = false
                                                                          });

                return serviceBusReceiver;
            });

            services.AddSingleton<IEventBus, AzureServiceBusEventBus>();


            return services;
        }
    }
}
