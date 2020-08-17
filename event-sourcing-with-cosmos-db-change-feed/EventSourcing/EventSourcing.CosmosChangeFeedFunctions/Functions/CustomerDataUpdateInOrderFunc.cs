using Azure;
using Azure.Cosmos;
using EventSourcing.CosmosChangeFeedFunctions.Model;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventSourcing.CosmosChangeFeedFunctions.Functions
{
    public class CustomerDataUpdateInOrderFunc
    {
        private readonly CosmosClient _client;
        public CustomerDataUpdateInOrderFunc(CosmosClient client)
        {
            _client = client;
        }

        [FunctionName("update-customer-data-in-order-func")]
        public async Task RunAsync([CosmosDBTrigger(
            databaseName: "cars-island-eshop",
            collectionName: "Customer",
            ConnectionStringSetting = "CosmosDbConnectionString",
            CreateLeaseCollectionIfNotExists = true,
            LeaseCollectionName = "Lease",
            LeaseCollectionPrefix = "UpdateCustomerDataInOrder")]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);

                foreach (var document in input)
                {
                    var customer = JsonSerializer.Deserialize<Customer>(document.ToString());
                    var customerShippingAddress = customer.ShippingAddress;
                    var customerId = customer.Id;

                    await UpdateShippingAddressInOrdersAsync(customerShippingAddress, customerId, log);
                }

            }
        }

        private CosmosContainer GetContainer(string databaseName, string collectionName)
        {
            var database = _client.GetDatabase(databaseName);
            var container = database.GetContainer(collectionName);
            return container;
        }

        private async Task UpdateShippingAddressInOrdersAsync(string customerShippingAddress, string customerId, ILogger log)
        {
            var container = GetContainer("cars-island-eshop", "Order");
            var sqlQueryText = $"SELECT * FROM c WHERE c.customerId = '{customerId}'";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            AsyncPageable<Order> queryResultSetIterator = container.GetItemQueryIterator<Order>(queryDefinition);
            var iterator = queryResultSetIterator.GetAsyncEnumerator();

            try
            {
                while (await iterator.MoveNextAsync())
                {
                    var currentOrder = iterator.Current;
                    currentOrder.ShippingAddress = customerShippingAddress;

                    await container
                         .ReplaceItemAsync(currentOrder, currentOrder.Id, new Azure.Cosmos.PartitionKey(currentOrder.Id));
                }
            }

            catch (CosmosException ex)
            {
                log.LogError($"An error has occurred during the update of orders for customer with id: {customerId}", ex);
            }

            finally
            {
                if (iterator != null)
                {

                    await iterator.DisposeAsync();
                }
            }
        }
    }
}
