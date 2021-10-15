using System;
using System.Text.Json.Serialization;

namespace TMF.ServiceBusReceiver.Common
{
    public abstract record IntegrationEvent
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }

        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; init; }
    }
}
