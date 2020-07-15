using CarsIsland.Catalog.Infrastructure.Configuration.Interfaces;
using CarsIsland.Catalog.Infrastructure.Services.Integration;
using CarsIsland.Catalog.Infrastructure.Services.Integration.Interfaces;
using CarsIsland.EventBus;
using CarsIsland.EventBus.Events.Interfaces;
using CarsIsland.EventBus.Services;
using CarsIsland.EventBus.Services.Interfaces;
using CarsIsland.EventLog.Services;
using CarsIsland.EventLog.Services.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;

namespace CarsIsland.Catalog.API.Core.DependencyInjection
{
    public static class IntegrationServiceCollectionExtensions
    {
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var azureServiceBusConfiguration = serviceProvider.GetRequiredService<IAzureServiceBusConfiguration>();

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

            services.AddSingleton<IServiceBusConnectionManagementService>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<ServiceBusConnectionManagementService>>();
                var serviceBusConnection = new ServiceBusConnectionStringBuilder(azureServiceBusConfiguration.ConnectionString);
                return new ServiceBusConnectionManagementService(logger, serviceBusConnection);
            });

            services.AddSingleton<IEventBus, AzureServiceBusEventBus>(sp =>
            {
                var serviceBusConnectionManagementService = sp.GetRequiredService<IServiceBusConnectionManagementService>();
                var logger = sp.GetRequiredService<ILogger<AzureServiceBusEventBus>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var eventBus = new AzureServiceBusEventBus(serviceBusConnectionManagementService, eventBusSubcriptionsManager,
                    serviceProvider, logger, azureServiceBusConfiguration.SubscriptionClientName);
                eventBus.SetupAsync().GetAwaiter().GetResult();

                return eventBus;
            });

            services.AddTransient<Func<DbConnection, IEventLogService>>(
                    sp => (DbConnection connection) => new EventLogService(connection));

            return services;
        }
    }
}
