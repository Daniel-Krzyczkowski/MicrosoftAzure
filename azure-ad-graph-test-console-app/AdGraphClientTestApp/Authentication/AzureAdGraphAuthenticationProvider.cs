using AdGraphClientTestApp.Configuration;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdGraphClientTestApp.Authentication
{
    public class AzureAdGraphAuthenticationProvider : IAuthenticationProvider
    {
        private readonly AzureAdGraphSettings _settings;

        public AzureAdGraphAuthenticationProvider(AzureAdGraphSettings settings)
        {
            _settings = settings;
        }

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var authContext = new AuthenticationContext($"https://login.microsoftonline.com/{_settings.AzureAdB2CTenant}/oauth2/v2.0/token");

            var clientCred = new ClientCredential(_settings.ClientId, _settings.ClientSecret);

            try
            {
                var authResult = await authContext.AcquireTokenAsync("https://graph.microsoft.com", clientCred);
                if (authResult == null)
                    throw new InvalidOperationException("Failed to obtain the JWT token");

                request.Headers.Add("Authorization", "Bearer " + authResult.AccessToken);
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
