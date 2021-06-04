using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMF.Secure.API.SecretManagement;

namespace TMF.Secure.API.Core.DependencyInjection
{
    public static class SecretServiceCollectionExtensions
    {
        public static IServiceCollection AddSecretServices(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            services.AddAzureClients(builder =>
            {
                builder.AddSecretClient(configuration.GetSection("KeyVault"));
            });

            services.AddSingleton<ISecretManager, SecretManager>();

            return services;
        }
    }
}
