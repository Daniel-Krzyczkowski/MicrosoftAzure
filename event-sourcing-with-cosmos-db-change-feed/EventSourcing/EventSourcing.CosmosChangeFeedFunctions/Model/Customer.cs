
using System.Text.Json.Serialization;

namespace EventSourcing.CosmosChangeFeedFunctions.Model
{
    internal class Customer
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("shippingAddress")]
        public string ShippingAddress { get; set; }
        [JsonPropertyName("isPremiumCustomer")]
        public bool IsPremiumCustomer { get; set; }
    }
}
