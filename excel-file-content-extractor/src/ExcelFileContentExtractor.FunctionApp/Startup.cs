using Azure.Cosmos;
using Azure.Cosmos.Serialization;
using ExcelFileContentExtractor.Core.Model;
using ExcelFileContentExtractor.Infrastructure.Configuration;
using ExcelFileContentExtractor.Infrastructure.Configuration.Interfaces;
using ExcelFileContentExtractor.Infrastructure.Services;
using ExcelFileContentExtractor.Infrastructure.Services.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

[assembly: FunctionsStartup(typeof(ExcelFileContentExtractor.FunctionApp.Startup))]
namespace ExcelFileContentExtractor.FunctionApp
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureSettings(builder);

            var serviceProvider = builder.Services.BuildServiceProvider();
            var cosmosDbSettings = serviceProvider.GetRequiredService<ICosmosDbDataServiceConfiguration>();
            CosmosClientOptions cosmosClientOptions = new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            };
            CosmosClient cosmosClient = new CosmosClient(cosmosDbSettings.ConnectionString, cosmosClientOptions);
            CosmosDatabase database = cosmosClient.CreateDatabaseIfNotExistsAsync(cosmosDbSettings.DatabaseName)
                                                   .GetAwaiter()
                                                   .GetResult();
            CosmosContainer container = database.CreateContainerIfNotExistsAsync(
                cosmosDbSettings.ContainerName,
                cosmosDbSettings.PartitionKeyPath,
                400)
                .GetAwaiter()
                .GetResult();

            builder.Services.AddSingleton(cosmosClient);
            builder.Services.AddSingleton<IFileExtensionValidationService, FileExtensionValidationService>();
            builder.Services.AddSingleton(typeof(IDataService<ExcelFileRawDataModel>), typeof(CosmosDbDataService<ExcelFileRawDataModel>));
            builder.Services.AddTransient<IExcelFileContentExtractorService, ExcelFileContentExtractorService>();
        }

        private void ConfigureSettings(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Environment.CurrentDirectory)
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            builder.Services.Configure<CosmosDbDataServiceConfiguration>(config.GetSection("AzureCosmosDbSettings"));
            builder.Services.AddSingleton<IValidateOptions<CosmosDbDataServiceConfiguration>, CosmosDbDataServiceConfigurationValidation>();
            var cosmosDbDataServiceConfiguration = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<CosmosDbDataServiceConfiguration>>().Value;
            builder.Services.AddSingleton<ICosmosDbDataServiceConfiguration>(cosmosDbDataServiceConfiguration);
        }
    }
}
