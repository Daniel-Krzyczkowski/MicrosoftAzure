using System;

namespace CarsIsland.Catalog.Domain.Model
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
