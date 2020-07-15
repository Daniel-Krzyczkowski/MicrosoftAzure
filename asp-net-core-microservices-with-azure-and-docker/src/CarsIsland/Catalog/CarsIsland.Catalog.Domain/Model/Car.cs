using System;

namespace CarsIsland.Catalog.Domain.Model
{
    public class Car : IEntity
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public bool AvailableForRent { get; set; }
    }
}
