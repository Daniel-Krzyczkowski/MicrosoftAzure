using CarsIsland.Rent.Domain.AggregatesModel.CarRentAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsIsland.Rent.Infrastructure.Repositories.Interfaces
{
    public interface IRentalRepository
    {
        Task<CarRent> GetByIdAsync(Guid id);
        Task<IReadOnlyList<CarRent>> ListAllAsync();
        CarRent Add(CarRent carRent);
        void Update(CarRent carRent);
        void Delete(CarRent carRent);
    }
}
