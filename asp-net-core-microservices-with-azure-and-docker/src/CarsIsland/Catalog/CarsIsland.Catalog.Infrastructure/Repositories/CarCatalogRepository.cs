using CarsIsland.Catalog.Domain.Model;
using CarsIsland.Catalog.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsIsland.Catalog.Infrastructure.Repositories
{
    public sealed class CarCatalogRepository : ICarsCatalogRepository
    {
        private readonly CarCatalogDbContext _sqlDbContext;
        private readonly ILogger<CarCatalogRepository> _logger;

        public CarCatalogRepository(CarCatalogDbContext sqlDbContext, ILogger<CarCatalogRepository> logger)
        {
            _sqlDbContext = sqlDbContext;
            _logger = logger;
        }

        public Car Add(Car car)
        {
            car.Id = Guid.NewGuid();
            return _sqlDbContext.Cars.Add(car).Entity;
        }

        public void Delete(Car car)
        {
            _sqlDbContext.Cars.Remove(car);
        }

        public async Task<Car> GetByIdAsync(Guid id)
        {
            var car = await _sqlDbContext.Cars
                                    .Where(e => e.Id == id)
                                    .FirstOrDefaultAsync();
            return car;

        }

        public void Update(Car car)
        {
            _sqlDbContext.Update(_sqlDbContext);
        }

        public async Task<IReadOnlyList<Car>> ListAllAsync()
        {
            var cars = await _sqlDbContext.Cars
                             .ToListAsync();
            return cars;
        }
    }
}
