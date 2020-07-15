using CarsIsland.Catalog.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarsIsland.Catalog.Infrastructure.Repositories
{
    public class CarCatalogDbContext : DbContext
    {
        public CarCatalogDbContext(DbContextOptions<CarCatalogDbContext> options)
                                                              : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>().HasData(
                    new Car
                    {
                        Id = Guid.NewGuid(),
                        Brand = "BMW",
                        Model = "320",
                        AvailableForRent = true,
                        PricePerDay = 200
                    },
                    new Car
                    {
                        Id = Guid.NewGuid(),
                        Brand = "Audi",
                        Model = "A1",
                        AvailableForRent = true,
                        PricePerDay = 120
                    },
                    new Car
                    {
                        Id = Guid.NewGuid(),
                        Brand = "Mercedes",
                        Model = "E200",
                        AvailableForRent = true,
                        PricePerDay = 250
                    },
                    new Car
                    {
                        Id = Guid.NewGuid(),
                        Brand = "Ford",
                        Model = "Focus",
                        AvailableForRent = true,
                        PricePerDay = 90
                    }
                );
        }
    }
}
