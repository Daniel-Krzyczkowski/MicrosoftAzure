using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureSignalRTransportApp.WebAPI.Model
{
    public class Summary
    {
        public int lengthInMeters { get; set; }
        public int travelTimeInSeconds { get; set; }
        public int trafficDelayInSeconds { get; set; }
        public DateTime departureTime { get; set; }
        public DateTime arrivalTime { get; set; }
    }

    public class Summary2
    {
        public int lengthInMeters { get; set; }
        public int travelTimeInSeconds { get; set; }
        public int trafficDelayInSeconds { get; set; }
        public DateTime departureTime { get; set; }
        public DateTime arrivalTime { get; set; }
    }

    public class Point
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Leg
    {
        public Summary2 summary { get; set; }
        public List<Point> points { get; set; }
    }

    public class Section
    {
        public int startPointIndex { get; set; }
        public int endPointIndex { get; set; }
        public string sectionType { get; set; }
        public string travelMode { get; set; }
    }

    public class Route
    {
        public Summary summary { get; set; }
        public List<Leg> legs { get; set; }
        public List<Section> sections { get; set; }
    }

    public class DirectionsResponse
    {
        public string formatVersion { get; set; }
        public string copyright { get; set; }
        public string privacy { get; set; }
        public List<Route> routes { get; set; }
    }
}
