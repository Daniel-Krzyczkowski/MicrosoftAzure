using AzureSignalRTransportApp.WebAPI.Config;
using AzureSignalRTransportApp.WebAPI.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzureSignalRTransportApp.WebAPI.Services
{

    public interface IMapService
    {
        Task<DirectionsResponse> GetDirections(DirectionsRequest directionsRequest);
    }

    public class MapService : IMapService
    {
        private AzureMapsSettings _azureMapsSettings;
        private RestClient _restClient;

        public MapService(IOptions<AzureMapsSettings> azureMapSettings)
        {
            _azureMapsSettings = azureMapSettings.Value;
            _restClient = new RestClient("https://atlas.microsoft.com");
        }

        public async Task<DirectionsResponse> GetDirections(DirectionsRequest directionsRequest)
        {
            var request = new RestRequest("route/directions/json", Method.GET);
            request.AddQueryParameter("subscription-key", _azureMapsSettings.SubscriptionKey);
            request.AddQueryParameter("api-version", "1.0");
            request.AddQueryParameter("query", $"{directionsRequest.FromLatitude},{directionsRequest.FromLongitude}:{directionsRequest.ToLatitude},{directionsRequest.ToLongitude}");
            var response = await _restClient.ExecuteTaskAsync<string>(request, default(CancellationToken));
            var directions = JsonConvert.DeserializeObject<DirectionsResponse>(response.Data);
            return directions;
        }
    }
}
