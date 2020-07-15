using System;

namespace CarsIsland.Reservation.Domain.Model
{
    public class CustomerReservation
    {
        public Guid CustomerId { get; set; }
        public ReservedCar Car { get; set; }
        public DateTime RentFrom { get; set; }
        public DateTime RentTo { get; set; }
        public decimal Price => Car.RentForPeriodInDays * Car.PricePerDay;

        public CustomerReservation(Guid customerId, ReservedCar car)
        {
            CustomerId = customerId;
            Car = car;
        }
    }
}
