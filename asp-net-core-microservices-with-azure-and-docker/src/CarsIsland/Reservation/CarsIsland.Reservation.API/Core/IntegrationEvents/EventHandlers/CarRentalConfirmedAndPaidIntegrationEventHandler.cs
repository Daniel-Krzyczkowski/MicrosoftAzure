using CarsIsland.EventBus.Events.Interfaces;
using CarsIsland.Reservation.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CarsIsland.Reservation.API.Core.IntegrationEvents.Events
{
    public class CarRentalConfirmedAndPaidIntegrationEventHandler : IIntegrationEventHandler<CarRentalConfirmedAndPaidIntegrationEvent>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger<CarRentalConfirmedAndPaidIntegrationEventHandler> _logger;

        public CarRentalConfirmedAndPaidIntegrationEventHandler(
            IReservationRepository reservationRepository,
            ILogger<CarRentalConfirmedAndPaidIntegrationEventHandler> logger)
        {
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleAsync(CarRentalConfirmedAndPaidIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
            await _reservationRepository.DeleteReservationAsync(@event.UserId.ToString());
        }
    }
}
