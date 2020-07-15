using Newtonsoft.Json;
using System;

namespace CarsIsland.EventBus.Events
{
    public abstract class IntegrationEvent
    {
        [JsonConstructor]
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonProperty]
        public Guid Id { get; private set; }

        [JsonProperty]
        public DateTime CreationDate { get; private set; }
    }
}
