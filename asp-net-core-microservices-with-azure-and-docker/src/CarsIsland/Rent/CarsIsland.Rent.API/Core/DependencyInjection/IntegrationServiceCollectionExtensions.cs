using CarsIsland.EventLog.Services;
using CarsIsland.EventLog.Services.Interfaces;
using CarsIsland.Rent.API.Core.IntegrationEvents;
using CarsIsland.Rent.API.Core.IntegrationEvents.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Common;

namespace CarsIsland.Rent.API.Core.DependencyInjection
{
    public static class IntegrationServiceCollectionExtensions
    {
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            services.AddTransient<Func<DbConnection, IEventLogService>>(
                    sp => (DbConnection connection) => new EventLogService(connection));

            services.AddTransient<IRentalIntegrationEventService, RentalIntegrationEventService>();

            return services;
        }
    }
}
