using CarsIsland.EventBus.Events;
using System.Threading.Tasks;

namespace CarsIsland.Catalog.Infrastructure.Services.Integration.Interfaces
{
    public interface ICatalogIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(IntegrationEvent @event);
        Task AddAndSaveEventAsync(IntegrationEvent @event);
    }
}
