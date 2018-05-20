using AzureSignalRTransportApp.WebAPI.Model;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureSignalRTransportApp.WebAPI.Hubs
{
    public class TransportHub : Hub
    {
        /// <summary>
        /// Handle location update message and broadcast it to all connected clients.
        /// </summary>
        /// <param name="locationUpdate"></param>
        public void BroadcastMessage(LocationUpdate locationUpdate)
        {
            Clients.All.SendAsync("broadcastMessage", locationUpdate);
        }
    }
}
