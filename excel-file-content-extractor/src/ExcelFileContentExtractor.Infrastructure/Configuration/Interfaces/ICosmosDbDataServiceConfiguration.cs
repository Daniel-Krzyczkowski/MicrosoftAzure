namespace ExcelFileContentExtractor.Infrastructure.Configuration.Interfaces
{
    public interface ICosmosDbDataServiceConfiguration
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ContainerName { get; set; }
        string PartitionKeyPath { get; set; }
    }
}
