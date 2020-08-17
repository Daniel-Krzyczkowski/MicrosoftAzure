using Azure.Cosmos;
using EventSourcing.CosmosChangeFeedFunctions.Model;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventSourcing.CosmosChangeFeedFunctions.Functions
{
    public class OrderHistoryPersistenceFunc
    {
        private readonly CosmosClient _client;
        public OrderHistoryPersistenceFunc(CosmosClient client)
        {
            _client = client;
        }

        [FunctionName("store-completed-order-func")]
        public async Task RunAsync([CosmosDBTrigger(
            databaseName: "cars-island-eshop",
            collectionName: "Order",
            ConnectionStringSetting = "CosmosDbConnectionString",
            CreateLeaseCollectionIfNotExists = true,
            LeaseCollectionName = "Lease",
            LeaseCollectionPrefix = "OrderHistoryPersistence")]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                foreach (var document in input)
                {
                    var order = JsonSerializer.Deserialize<Order>(document.ToString());
                    await UpdateOrderHistoryAsync(order, log);
                }
            }
        }

        private CosmosContainer GetContainer(string databaseName, string collectionName)
        {
            var database = _client.GetDatabase(databaseName);
            var container = database.GetContainer(collectionName);
            return container;
        }

        private async Task UpdateOrderHistoryAsync(Order order, ILogger log)
        {
            var container = GetContainer("cars-island-eshop", "OrderHistory");

            try
            {
                var orderHistory = new OrderHistory
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerId = order.CustomerId,
                    CreationDate = DateTime.UtcNow,
                    OrderId = order.Id,
                    PaymentMethod = order.PaymentMethod,
                    Products = order.Products,
                    TotalPrice = order.TotalPrice
                };

                await container
                     .CreateItemAsync(orderHistory, new Azure.Cosmos.PartitionKey(orderHistory.OrderId));
            }

            catch (CosmosException ex)
            {
                log.LogError($"An error has occurred during creation of order history record for order with id: {order.Id}", ex);
            }
        }
    }
}
