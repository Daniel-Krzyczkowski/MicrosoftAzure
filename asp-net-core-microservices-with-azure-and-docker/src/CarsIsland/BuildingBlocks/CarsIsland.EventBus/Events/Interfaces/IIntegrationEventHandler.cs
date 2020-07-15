using System.Threading.Tasks;

namespace CarsIsland.EventBus.Events.Interfaces
{
    public interface IIntegrationEventHandler<in TIntegrationEvent>
        where TIntegrationEvent : IntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent @event);
    }
}
