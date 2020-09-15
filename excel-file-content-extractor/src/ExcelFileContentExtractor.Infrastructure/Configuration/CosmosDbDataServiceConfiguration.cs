using ExcelFileContentExtractor.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Options;

namespace ExcelFileContentExtractor.Infrastructure.Configuration
{
    public class CosmosDbDataServiceConfiguration : ICosmosDbDataServiceConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ContainerName { get; set; }
        public string PartitionKeyPath { get; set; }
    }

    public class CosmosDbDataServiceConfigurationValidation : IValidateOptions<CosmosDbDataServiceConfiguration>
    {
        public ValidateOptionsResult Validate(string name, CosmosDbDataServiceConfiguration options)
        {
            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ConnectionString)} configuration parameter for the Azure Cosmos DB is required");
            }

            if (string.IsNullOrEmpty(options.ContainerName))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ContainerName)} configuration parameter for the Azure Cosmos DB is required");
            }

            if (string.IsNullOrEmpty(options.DatabaseName))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.DatabaseName)} configuration parameter for the Azure Cosmos DB is required");
            }

            if (string.IsNullOrEmpty(options.PartitionKeyPath))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.PartitionKeyPath)} configuration parameter for the Azure Cosmos DB is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
