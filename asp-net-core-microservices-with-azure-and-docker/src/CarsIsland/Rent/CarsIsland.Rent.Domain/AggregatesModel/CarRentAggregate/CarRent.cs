using CarsIsland.Rent.Domain.Common;
using System;

namespace CarsIsland.Rent.Domain.AggregatesModel.CarRentAggregate
{
    public class CarRent : Entity, IAggregateRoot
    {
        public Guid CustomerId => _customerId;
        private Guid _customerId;

        private DateTime _rentFrom { get; set; }
        private DateTime _rentTo { get; set; }
        private Guid _paymentId;
        private readonly CarRentStatus _carRentStatus;

        public void SetPaymentId(Guid id)
        {
            _paymentId = id;
        }

        public void SetCustomerId(Guid id)
        {
            _customerId = id;
        }
    }
}
