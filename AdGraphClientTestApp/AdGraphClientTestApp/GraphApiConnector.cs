using AdGraphClientTestApp.Configuration;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdGraphClientTestApp
{
    public interface IGraphApiConnector
    {
        GraphServiceClient GetAuthenticatedGraphServiceClient(bool shouldUseBetaEndpoint);
    }

    public class GraphApiConnector : IGraphApiConnector
    {
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly AzureAdGraphSettings _settings;

        public GraphApiConnector(IAuthenticationProvider authenticationProvider,
            AzureAdGraphSettings settings)
        {
            _authenticationProvider = authenticationProvider;
            _settings = settings;
        }

        public GraphServiceClient GetAuthenticatedGraphServiceClient(bool shouldUseBetaEndpoint)
        {
            if(!shouldUseBetaEndpoint)
            {
                return new GraphServiceClient($"{_settings.GraphApiBaseUrl}{_settings.ApiVersion}", _authenticationProvider);
            }
            else
            {
                return new GraphServiceClient($"{_settings.BetaGraphApiBaseUrl}", _authenticationProvider);
            }
        }
    }
}
