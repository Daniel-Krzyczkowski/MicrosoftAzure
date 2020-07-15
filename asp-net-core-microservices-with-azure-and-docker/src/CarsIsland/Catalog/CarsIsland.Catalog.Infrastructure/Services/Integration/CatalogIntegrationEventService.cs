using CarsIsland.Catalog.Infrastructure.Repositories;
using CarsIsland.Catalog.Infrastructure.Services.Integration.Interfaces;
using CarsIsland.EventBus.Events;
using CarsIsland.EventBus.Events.Interfaces;
using CarsIsland.EventLog;
using CarsIsland.EventLog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace CarsIsland.Catalog.Infrastructure.Services.Integration
{
    public class CatalogIntegrationEventService : ICatalogIntegrationEventService
    {
        private readonly CarCatalogDbContext _carCatalogDbContext;
        private readonly IEventBus _eventBus;
        private readonly IEventLogService _eventLogService;
        private readonly ILogger<CatalogIntegrationEventService> _logger;
        private readonly Func<DbConnection, IEventLogService> _integrationEventLogServiceFactory;

        public CatalogIntegrationEventService(CarCatalogDbContext carCatalogDbContext, Func<DbConnection, IEventLogService> integrationEventLogServiceFactory,
                                     IEventBus eventBus,
                                     ILogger<CatalogIntegrationEventService> logger)
        {
            _carCatalogDbContext = carCatalogDbContext ?? throw new ArgumentNullException(nameof(carCatalogDbContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_carCatalogDbContext.Database.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent @event)
        {
            await ResilientTransaction.CreateNew(_carCatalogDbContext).ExecuteAsync(async () =>
            {
                await _carCatalogDbContext.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(@event, _carCatalogDbContext.Database.CurrentTransaction);
            });
        }

        public async Task PublishEventsThroughEventBusAsync(IntegrationEvent @event)
        {
            try
            {
                await _eventLogService.MarkEventAsInProgressAsync(@event.Id);
                await _eventBus.PublishAsync(@event);
                await _eventLogService.MarkEventAsPublishedAsync(@event.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR publishing integration event: '{IntegrationEventId}'", @event.Id);

                await _eventLogService.MarkEventAsFailedAsync(@event.Id);
            }
        }
    }
}
