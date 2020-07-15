using CarsIsland.Reservation.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Options;

namespace CarsIsland.Reservation.Infrastructure.Configuration
{
    public class RedisConfiguration : IRedisConfiguration
    {
        public string ConnectionString { get; set; }
    }

    public class RedisConfigurationValidation : IValidateOptions<RedisConfiguration>
    {
        public ValidateOptionsResult Validate(string name, RedisConfiguration options)
        {
            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ConnectionString)} configuration parameter for the Azure Redis Cache is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
