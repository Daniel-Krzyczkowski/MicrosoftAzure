using System;
using System.Text.Json.Serialization;

namespace EventSourcing.CosmosChangeFeedFunctions.Model
{
    internal class OrderHistory
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }

        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }

        [JsonPropertyName("products")]
        public Product[] Products { get; set; }

        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("totalPrice")]
        public long TotalPrice { get; set; }

        [JsonPropertyName("creationDate")]
        public DateTimeOffset CreationDate { get; set; }
    }
}
