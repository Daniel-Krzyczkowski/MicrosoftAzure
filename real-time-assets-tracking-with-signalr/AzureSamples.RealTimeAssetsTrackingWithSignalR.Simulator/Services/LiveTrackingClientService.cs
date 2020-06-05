using AzureSamples.RealTimeAssetsTrackingWithSignalR.Simulator.Model;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureSamples.RealTimeAssetsTrackingWithSignalR.Simulator.Services
{
    class LiveTrackingClientService
    {
        private HubConnection _hub;
        public HubConnection Hub
        {
            get
            {
                return _hub;
            }
        }

        private string _connectionUrl;
        public string ConnectionUrl
        {
            get
            {
                return _connectionUrl;
            }
        }

        /// <summary>
        /// Initialize connection with hub.
        /// </summary>
        /// <param name="connectionUrl"></param>
        /// <returns></returns>
        public async Task Initialize(string connectionUrl)
        {
            _connectionUrl = connectionUrl;

            _hub = new HubConnectionBuilder()
                .WithUrl(_connectionUrl)
                .Build();

            await _hub.StartAsync();
        }

        /// <summary>
        /// Subscribe to receive updates from the hub.
        /// </summary>
        /// <param name="methodName"></param>
        public void SubscribeHubMethod(string methodName)
        {
            _hub.On<LocationUpdate>(methodName, (locationUpdate) =>
            {
                OnMessageReceived?.Invoke(locationUpdate);
            });
        }

        /// <summary>
        /// Send new message to hub.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="locationUpdate"></param>
        /// <returns></returns>
        public async Task SendHubMessage(string methodName, LocationUpdate locationUpdate)
        {
            await _hub?.InvokeAsync(methodName, locationUpdate);
        }

        /// <summary>
        /// Close connection between client and hub.
        /// </summary>
        /// <returns></returns>
        public async Task CloseConnection()
        {
            await _hub.DisposeAsync();
        }

        public event Action<LocationUpdate> OnMessageReceived;
    }
}
