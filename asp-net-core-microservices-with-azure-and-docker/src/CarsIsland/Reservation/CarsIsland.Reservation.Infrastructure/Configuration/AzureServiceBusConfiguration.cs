using CarsIsland.Reservation.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Options;

namespace CarsIsland.Catalog.Infrastructure.Configuration
{
    public class AzureServiceBusConfiguration : IAzureServiceBusConfiguration
    {
        public string ConnectionString { get; set; }
        public string SubscriptionClientName { get; set; }
    }

    public class AzureServiceBusConfigurationValidation : IValidateOptions<AzureServiceBusConfiguration>
    {
        public ValidateOptionsResult Validate(string name, AzureServiceBusConfiguration options)
        {
            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ConnectionString)} configuration parameter for the Azure Service Bus is required");
            }

            if (string.IsNullOrEmpty(options.SubscriptionClientName))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.SubscriptionClientName)} configuration parameter for the Azure Service Bus is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
