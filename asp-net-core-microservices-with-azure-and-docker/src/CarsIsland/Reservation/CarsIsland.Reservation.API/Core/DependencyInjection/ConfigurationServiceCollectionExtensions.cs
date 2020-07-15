using CarsIsland.Catalog.Infrastructure.Configuration;
using CarsIsland.Reservation.Infrastructure.Configuration;
using CarsIsland.Reservation.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CarsIsland.Reservation.API.Core.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<RedisConfiguration>(config.GetSection("Redis"));
            services.AddSingleton<IValidateOptions<RedisConfiguration>, RedisConfigurationValidation>();
            var redisconfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<RedisConfiguration>>().Value;
            services.AddSingleton<IRedisConfiguration>(redisconfiguration);

            services.Configure<AzureServiceBusConfiguration>(config.GetSection("AzureServiceBusSettings"));
            services.AddSingleton<IValidateOptions<AzureServiceBusConfiguration>, AzureServiceBusConfigurationValidation>();
            var azureServiceBusConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<AzureServiceBusConfiguration>>().Value;
            services.AddSingleton<IAzureServiceBusConfiguration>(azureServiceBusConfiguration);

            return services;
        }
    }
}
