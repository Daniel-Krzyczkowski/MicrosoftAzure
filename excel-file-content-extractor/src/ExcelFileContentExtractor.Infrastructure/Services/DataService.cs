using Azure.Cosmos;
using ExcelFileContentExtractor.Core.Model;
using ExcelFileContentExtractor.Infrastructure.Configuration.Interfaces;
using ExcelFileContentExtractor.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ExcelFileContentExtractor.Infrastructure.Services
{
    public sealed class CosmosDbDataService<T> : IDataService<T> where T : class, IEntity
    {
        private readonly ICosmosDbDataServiceConfiguration _dataServiceConfiguration;
        private readonly CosmosClient _client;
        private readonly ILogger<CosmosDbDataService<T>> _logger;

        public CosmosDbDataService(ICosmosDbDataServiceConfiguration dataServiceConfiguration,
                                   CosmosClient client,
                                   ILogger<CosmosDbDataService<T>> logger)
        {
            _dataServiceConfiguration = dataServiceConfiguration;
            _client = client;
            _logger = logger;
        }

        public async Task<T> AddAsync(T newEntity)
        {
            try
            {
                var container = GetContainer();
                ItemResponse<T> createResponse = await container.CreateItemAsync(newEntity);
                return createResponse.Value;
            }
            catch (CosmosException ex)
            {
                _logger.LogError($"New entity with ID: {newEntity.Id} was not added successfully - error details: {ex.Message}");
                throw;
            }
        }


        private CosmosContainer GetContainer()
        {
            var database = _client.GetDatabase(_dataServiceConfiguration.DatabaseName);
            var container = database.GetContainer(_dataServiceConfiguration.ContainerName);
            return container;
        }
    }
}
