using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdGraphClientTestApp.Model.Audit
{
    public class AuditLogsData
    {
        [JsonProperty("@odata.context")]
        public string DataContext { get; set; }
        public List<Value> value { get; set; }
    }

    public class App
    {
        public string appId { get; set; }
        public object displayName { get; set; }
        public object servicePrincipalId { get; set; }
        public string servicePrincipalName { get; set; }
    }

    public class User
    {
        public object id { get; set; }
        public object displayName { get; set; }
        public string userPrincipalName { get; set; }
        public string ipAddress { get; set; }
    }

    public class InitiatedBy
    {
        public App app { get; set; }
        public User user { get; set; }
    }

    public class TargetResource
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public List<object> modifiedProperties { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public string category { get; set; }
        public string correlationId { get; set; }
        public string result { get; set; }
        public string resultReason { get; set; }
        public string activityDisplayName { get; set; }
        public DateTime activityDateTime { get; set; }
        public string loggedByService { get; set; }
        public InitiatedBy initiatedBy { get; set; }
        public List<TargetResource> targetResources { get; set; }
        public List<object> additionalDetails { get; set; }
    }

}
