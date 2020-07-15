using System;

namespace CarsIsland.Reservation.Domain.Model
{
    public class ReservedCar
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public int RentForPeriodInDays { get; set; }
    }
}
