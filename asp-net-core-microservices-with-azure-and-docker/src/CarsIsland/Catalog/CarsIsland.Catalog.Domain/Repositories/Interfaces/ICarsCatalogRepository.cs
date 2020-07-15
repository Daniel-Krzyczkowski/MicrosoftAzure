using CarsIsland.Catalog.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsIsland.Catalog.Domain.Repositories.Interfaces
{
    public interface ICarsCatalogRepository
    {
        Task<Car> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Car>> ListAllAsync();
        Car Add(Car car);
        void Update(Car car);
        void Delete(Car car);
    }
}
