using CarsIsland.EventBus.Services.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using System;

namespace CarsIsland.EventBus.Services
{
    public class ServiceBusConnectionManagementService : IServiceBusConnectionManagementService
    {
        private readonly ILogger<ServiceBusConnectionManagementService> _logger;
        private readonly ServiceBusConnectionStringBuilder _serviceBusConnectionStringBuilder;
        private ITopicClient _topicClient;

        public ServiceBusConnectionManagementService(ILogger<ServiceBusConnectionManagementService> logger,
                               ServiceBusConnectionStringBuilder serviceBusConnectionStringBuilder)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceBusConnectionStringBuilder = serviceBusConnectionStringBuilder ??
                throw new ArgumentNullException(nameof(serviceBusConnectionStringBuilder));
            _topicClient = new TopicClient(_serviceBusConnectionStringBuilder, RetryPolicy.Default);
        }

        public ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder => _serviceBusConnectionStringBuilder;

        public ITopicClient CreateTopicClient()
        {
            if (_topicClient.IsClosedOrClosing)
            {
                _topicClient = new TopicClient(_serviceBusConnectionStringBuilder, RetryPolicy.Default);
            }
            return _topicClient;
        }
    }
}
