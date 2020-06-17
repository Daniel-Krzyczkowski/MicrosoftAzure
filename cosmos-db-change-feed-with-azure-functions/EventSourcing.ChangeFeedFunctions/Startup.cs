using Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(EventSourcing.ChangeFeedFunctions.Startup))]
namespace EventSourcing.ChangeFeedFunctions
{
    internal class Startup : FunctionsStartup
    {
        private IConfiguration _configuration;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureSettings(builder);
            ConfigureCosmosDbClient(builder);
        }

        private void ConfigureSettings(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Environment.CurrentDirectory)
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();
            _configuration = config;
        }


        private void ConfigureCosmosDbClient(IFunctionsHostBuilder builder)
        {
            CosmosClient cosmosClient = new CosmosClient(_configuration.GetConnectionString("CosmosDbConnectionString"));

            builder.Services.AddSingleton(cosmosClient);
        }
    }
}
