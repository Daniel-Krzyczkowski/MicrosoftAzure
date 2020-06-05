using System.Threading.Tasks;
using AzureSamples.RealTimeAssetsTrackingWithSignalR.API.Model;
using Microsoft.AspNetCore.SignalR;

namespace AzureSamples.RealTimeAssetsTrackingWithSignalR.API.Hubs
{
    public class LiveTrackingHub : Hub
    {
        /// <summary>
        /// Handle location update message and broadcast it to all connected clients.
        /// </summary>
        /// <param name="locationUpdate"></param>
        [HubMethodName("location-update")]
        public Task LocationUpdate(LocationUpdate locationUpdate)
        {
            return Clients.All.SendAsync("location-update", locationUpdate);
        }
    }
}
