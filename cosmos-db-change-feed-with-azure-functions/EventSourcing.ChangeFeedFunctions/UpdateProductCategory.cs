using Azure;
using Azure.Cosmos;
using EventSourcing.ChangeFeedFunctions.Model;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSourcing.ChangeFeedFunctions
{
    public class UpdateProductCategory
    {
        private readonly CosmosClient _client;
        public UpdateProductCategory(CosmosClient client)
        {
            _client = client;
        }

        [FunctionName("update-product-category")]
        public async Task RunAsync([CosmosDBTrigger(
            databaseName: "",
            collectionName: "ProductCategory",
            ConnectionStringSetting = "CosmosDbConnectionString",
            LeaseCollectionName = "leases", CreateLeaseCollectionIfNotExists =true)]IReadOnlyList<Document>
            documents, ILogger log)
        {
            if (documents != null && documents.Count > 0)
            {
                log.LogInformation("Documents modified " + documents.Count);

                foreach (var document in documents)
                {
                    var productCategory = JsonConvert.DeserializeObject<ProductCategory>(document.ToString());
                    var productCategoryId = productCategory.id;
                    var productCategoryName = productCategory.name;

                    await UpdateProductsAsync(productCategoryId, productCategoryName, log);
                }

            }
        }

        private CosmosContainer GetContainer(string databaseName, string collectionName)
        {
            var database = _client.GetDatabase(databaseName);
            var container = database.GetContainer(collectionName);
            return container;
        }

        private async Task UpdateProductsAsync(string productCategoryId, string productCategoryName, ILogger log)
        {
            var container = GetContainer("clean-arch-db", "Product");
            var sqlQueryText = $"SELECT * FROM c WHERE c.categoryId = '{productCategoryId}'";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            AsyncPageable<Product> queryResultSetIterator = container.GetItemQueryIterator<Product>(queryDefinition);
            var iterator = queryResultSetIterator.GetAsyncEnumerator();

            try
            {
                while (await iterator.MoveNextAsync())
                {
                    var entity = iterator.Current;
                    entity.categoryName = productCategoryName;

                    await container
                         .ReplaceItemAsync(entity, entity.id.ToString(), new Azure.Cosmos.PartitionKey(productCategoryId));
                }
            }

            catch (CosmosException ex)
            {
                log.LogError($"An error has occurred during the update of product category with id: {productCategoryId}", ex);
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
