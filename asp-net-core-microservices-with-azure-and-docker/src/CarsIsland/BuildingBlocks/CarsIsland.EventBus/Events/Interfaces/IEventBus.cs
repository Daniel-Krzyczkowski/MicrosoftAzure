using System.Threading.Tasks;

namespace CarsIsland.EventBus.Events.Interfaces
{
    public interface IEventBus
    {
        Task PublishAsync(IntegrationEvent @event);

        Task SubscribeAsync<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        Task UnsubscribeAsync<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;

        Task SetupAsync();
    }
}
