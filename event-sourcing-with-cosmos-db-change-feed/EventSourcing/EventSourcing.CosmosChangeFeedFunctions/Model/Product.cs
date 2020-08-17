using System;
using System.Text.Json.Serialization;

namespace EventSourcing.CosmosChangeFeedFunctions.Model
{
    internal class Product
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("price")]
        public long Price { get; set; }

        [JsonPropertyName("quantity")]
        public long Quantity { get; set; }
    }
}
