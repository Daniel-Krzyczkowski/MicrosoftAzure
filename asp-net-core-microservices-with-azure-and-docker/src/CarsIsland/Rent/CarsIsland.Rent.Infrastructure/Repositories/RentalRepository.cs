using CarsIsland.Rent.Domain.AggregatesModel.CarRentAggregate;
using CarsIsland.Rent.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsIsland.Rent.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        public CarRent Add(CarRent carRent)
        {
            throw new NotImplementedException();
        }

        public void Delete(CarRent carRent)
        {
            throw new NotImplementedException();
        }

        public Task<CarRent> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CarRent>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(CarRent carRent)
        {
            throw new NotImplementedException();
        }
    }
}
