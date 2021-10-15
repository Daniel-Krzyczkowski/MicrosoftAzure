using System.Text.Json.Serialization;
using TMF.ServiceBusReceiver.Common;

namespace TMF.ServiceBusReceiver.API.Application.IntegrationEvents
{
    internal record FileSuccessfullyUploadedIntegrationEvent : IntegrationEvent
    {
        [JsonPropertyName("userId")]
        public string UserId { get; init; }
        [JsonPropertyName("fileUrl")]
        public string FileUrl { get; init; }
    }
}
