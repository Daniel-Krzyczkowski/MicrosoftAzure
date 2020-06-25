using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdGraphClientTestApp.Model
{
    public class SignInData
    {
        [JsonProperty("@odata.context")]
        public string DataContext { get; set; }
        public List<Value> value { get; set; }
    }

    public class MfaDetail
    {
        public object authMethod { get; set; }
        public object authDetail { get; set; }
    }

    public class Status
    {
        public int errorCode { get; set; }
        public string failureReason { get; set; }
        public string additionalDetails { get; set; }
    }

    public class DeviceDetail
    {
        public string deviceId { get; set; }
        public string displayName { get; set; }
        public string operatingSystem { get; set; }
        public string browser { get; set; }
        public bool? isCompliant { get; set; }
        public bool? isManaged { get; set; }
        public string trustType { get; set; }
    }

    public class GeoCoordinates
    {
        public object altitude { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Location
    {
        public string city { get; set; }
        public string state { get; set; }
        public string countryOrRegion { get; set; }
        public GeoCoordinates geoCoordinates { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public DateTime createdDateTime { get; set; }
        public string userDisplayName { get; set; }
        public string userPrincipalName { get; set; }
        public string userId { get; set; }
        public string appId { get; set; }
        public string appDisplayName { get; set; }
        public string ipAddress { get; set; }
        public string clientAppUsed { get; set; }
        public MfaDetail mfaDetail { get; set; }
        public string correlationId { get; set; }
        public object conditionalAccessStatus { get; set; }
        public object originalRequestId { get; set; }
        public bool isInteractive { get; set; }
        public object tokenIssuerName { get; set; }
        public string tokenIssuerType { get; set; }
        public int processingTimeInMilliseconds { get; set; }
        public string riskDetail { get; set; }
        public string riskLevelAggregated { get; set; }
        public string riskLevelDuringSignIn { get; set; }
        public string riskState { get; set; }
        public List<object> riskEventTypes { get; set; }
        public string resourceDisplayName { get; set; }
        public string resourceId { get; set; }
        public List<object> authenticationMethodsUsed { get; set; }
        public Status status { get; set; }
        public DeviceDetail deviceDetail { get; set; }
        public Location location { get; set; }
        public List<object> appliedConditionalAccessPolicies { get; set; }
        public List<object> authenticationProcessingDetails { get; set; }
        public List<object> networkLocationDetails { get; set; }
    }
}
