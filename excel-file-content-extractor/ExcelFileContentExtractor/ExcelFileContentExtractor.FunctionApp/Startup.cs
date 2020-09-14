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
            builder.Services.AddSingleton<IFileExtensionValidationService, FileExtensionValidationService>();
            builder.Services.AddSingleton<IDataService, DataService>();
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
            var sqlDbDataServiceConfiguration = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<CosmosDbDataServiceConfiguration>>().Value;
            builder.Services.AddSingleton<ICosmosDbDataServiceConfiguration>(sqlDbDataServiceConfiguration);
        }
    }
}
