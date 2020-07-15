using CarsIsland.Catalog.Domain.Repositories.Interfaces;
using CarsIsland.Catalog.Infrastructure.Configuration.Interfaces;
using CarsIsland.Catalog.Infrastructure.Repositories;
using CarsIsland.EventLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace CarsIsland.Catalog.API.Core.DependencyInjection
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataService(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var sqlDbConfiguration = serviceProvider.GetRequiredService<ISqlDbDataServiceConfiguration>();

            services.AddDbContext<CarCatalogDbContext>(options =>
            {
                options.UseSqlServer(sqlDbConfiguration.ConnectionString,
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(CarCatalogDbContext).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                                     });
            });


            services.AddDbContext<EventLogContext>(options =>
            {
                options.UseSqlServer(sqlDbConfiguration.ConnectionString,
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(CarCatalogDbContext).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                                     });
            });

            services.AddScoped<ICarsCatalogRepository, CarCatalogRepository>();

            return services;
        }
    }
}
