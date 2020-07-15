using Microsoft.Azure.ServiceBus;

namespace CarsIsland.EventBus.Services.Interfaces
{
    public interface IServiceBusConnectionManagementService
    {
        ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder { get; }

        ITopicClient CreateTopicClient();
    }
}
