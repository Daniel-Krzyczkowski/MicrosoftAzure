using System;
using System.Text.Json.Serialization;

namespace EventSourcing.CosmosChangeFeedFunctions.Model
{
    internal class Order
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }

        [JsonPropertyName("customerFirstName")]
        public string CustomerFirstName { get; set; }

        [JsonPropertyName("customerLastName")]
        public string CustomerLastName { get; set; }

        [JsonPropertyName("shippingAddress")]
        public string ShippingAddress { get; set; }

        [JsonPropertyName("products")]
        public Product[] Products { get; set; }

        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("orderDate")]
        public DateTimeOffset OrderDate { get; set; }

        [JsonPropertyName("totalPrice")]
        public long TotalPrice { get; set; }
    }
}
