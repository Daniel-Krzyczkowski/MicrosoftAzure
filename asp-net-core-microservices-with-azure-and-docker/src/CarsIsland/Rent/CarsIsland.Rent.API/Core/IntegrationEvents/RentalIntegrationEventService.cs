using CarsIsland.EventBus.Events;
using CarsIsland.EventBus.Events.Interfaces;
using CarsIsland.EventLog;
using CarsIsland.EventLog.Services.Interfaces;
using CarsIsland.Rent.API.Core.IntegrationEvents.Interfaces;
using CarsIsland.Rent.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace CarsIsland.Rent.API.Core.IntegrationEvents
{
    internal class RentalIntegrationEventService : IRentalIntegrationEventService
    {
        private readonly RentalDbContext _rentDbContext;
        private readonly IEventBus _eventBus;
        private readonly IEventLogService _eventLogService;
        private readonly ILogger<RentalIntegrationEventService> _logger;
        private readonly Func<DbConnection, IEventLogService> _integrationEventLogServiceFactory;

        public RentalIntegrationEventService(RentalDbContext rentDbContext, Func<DbConnection, IEventLogService> integrationEventLogServiceFactory,
                                             IEventBus eventBus, IEventLogService eventLogService,
                                             ILogger<RentalIntegrationEventService> logger)
        {
            _rentDbContext = rentDbContext ?? throw new ArgumentNullException(nameof(rentDbContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_rentDbContext.Database.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent @event)
        {
            await ResilientTransaction.CreateNew(_rentDbContext).ExecuteAsync(async () =>
            {
                await _rentDbContext.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(@event, _rentDbContext.Database.CurrentTransaction);
            });
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvent in pendingLogEvents)
            {
                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(logEvent.EventId);
                    await _eventBus.PublishAsync(logEvent.IntegrationEvent);
                    await _eventLogService.MarkEventAsPublishedAsync(logEvent.EventId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERROR publishing integration event: '{IntegrationEventId}'", logEvent.EventId);

                    await _eventLogService.MarkEventAsFailedAsync(logEvent.EventId);
                }
            }
        }
    }
}
