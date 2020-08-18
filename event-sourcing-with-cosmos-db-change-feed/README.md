<p align="center">
<img src="/assets/CosmosChangeFeedEventSourcing3.png?raw=true" alt="Image not found"/>
</p>

# Azure Function with Cosmos DB trigger

When talking about Azure Cosmos DB change feed, it is worth to mention that there are three possible ways to consume change feed:

1. **Directly** - using low-level direct access (not recommended because of complexity)
2. **Change Feed Processor Library (CFPL)** - stateful and scalable library provided for different languages, including .NET C#
3. **Azure Functions** - with change feed trigger configured

In this article we are going to use third possible option. To do it, let's create new Azure Function project in the Visual Studio:

<p align="center">
<img src="/assets/CosmosChangeFeedEventSourcing2.PNG?raw=true" alt="Image not found"/>
</p>

Once we create Azure Function with a change feed trigger we have to specify from which container we would like to receive notifications.

## Cosmos DB database containers structure

In this article we will work with some containers I created. These are:

<p align="center">
<img src="/assets/CosmosChangeFeedEventSourcing4.png?raw=true" alt="Image not found"/>
</p>

We are going to focus on changes in the *Customer* and *Order container*. We are going to use two Azure Functions with change feed triggers:

1. **CustomerDataUpdateInOrderFunc** - this function is responsible for updating customer data in all orders, once the data is changed in the *Customer* container
2. **OrderHistoryPersistenceFunc** - this function implements event sourcing, we want to track and store information about every order change


## CustomerDataUpdateInOrderFunc function app

This is the fragment of the source code of the first function:

```csharp
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
```

As we can see in the trigger parameters we have to specify to which database we would like to connect with information about the container. There is also *LeaseCollectionName* parameter which has to be specified. Lease collection is required to track changes related to change feed so change is always handled and processed. We can share lease container among functions but to do so, we have to also specify *LeaseCollectionPrefix* parameter. This will differentiate states for each change feed related to a specific container.

Once customer data is updated in the *Customer* container, we would like to get all orders related to this customer and update the shipping address for them:

```csharp
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
```

I used the new [Azure Cosmos DB SDK for .NET](https://www.nuget.org/packages/Azure.Cosmos) in this project. We use *CosmosClient* instance registered as a singleton to manage the propagation of changes to other containers in the Cosmos DB.

## OrderHistoryPersistenceFunc function app

This is the fragment of the source code of the second function:

```csharp
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
```

In this function, we react to changes in the *Order* container. In this case we would like to store information about any update related to order item. We store this information in the *OrderHistory* container then:

```csharp
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
```

Again, I used the new [Azure Cosmos DB SDK for .NET](https://www.nuget.org/packages/Azure.Cosmos) in this project. We use *CosmosClient* instance registered as a singleton to manage the propagation of changes to other containers in the Cosmos DB.