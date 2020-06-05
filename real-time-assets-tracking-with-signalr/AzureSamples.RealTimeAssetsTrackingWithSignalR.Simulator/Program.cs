using AzureSamples.RealTimeAssetsTrackingWithSignalR.Simulator.Model;
using AzureSamples.RealTimeAssetsTrackingWithSignalR.Simulator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AzureSamples.RealTimeAssetsTrackingWithSignalR.Simulator
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            List<LocationUpdate> locationUpdates = new List<LocationUpdate>
            {
             new LocationUpdate
                {
                    Latitude = 52.23292,
                    Longitude = 20.99114,
                    DriverName = "Daniel"
                },
             new LocationUpdate
                {
                    Latitude = 52.25229,
                    Longitude = 20.94341,
                    DriverName = "Daniel"
                },
             new LocationUpdate
                {
                    Latitude = 52.19462,
                    Longitude = 20.82109,
                    DriverName = "Daniel"
                },
             new LocationUpdate
                {
                    Latitude = 52.09011,
                    Longitude = 20.36584,
                    DriverName = "Daniel"
                },
             new LocationUpdate
                {
                    Latitude = 52.01813,
                    Longitude = 19.97684,
                    DriverName = "Daniel"
                },
             new LocationUpdate
                {
                    Latitude = 51.87744,
                    Longitude = 19.64773,
                    DriverName = "Daniel"
                },
             new LocationUpdate
                {
                    Latitude = 51.13844,
                    Longitude = 16.93472,
                    DriverName = "Daniel"
                },
             new LocationUpdate
                {
                    Latitude = 51.12705,
                    Longitude = 16.92196,
                    DriverName = "Daniel"
                },
            };

            var hubClient = new LiveTrackingClientService();
            await hubClient.Initialize("http://localhost:5000/live-tracking");

            Observable
            .Interval(TimeSpan.FromSeconds(3))
            .Subscribe(
                async x =>
                {
                    var locationUpdate = locationUpdates.FirstOrDefault();
                    if (locationUpdate != null)
                    {
                        await hubClient.SendHubMessage("location-update", locationUpdate);
                        Console.WriteLine("SENDING LOCATION UPDATE: " + locationUpdate.DriverName + " " + locationUpdate.Latitude + " " + locationUpdate.Longitude);
                        locationUpdates.Remove(locationUpdate);
                    }
                    else
                        Console.WriteLine("UPDATES COMPLETED");
                });

            Console.ReadKey();
        }
    }
}
