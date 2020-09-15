<p align="center">
<img src="assets/ExcelContentExtractionWithAzure2.png?raw=true" alt="Image not found"/>
</p>


# Introduction

Extracting file content in Azure sounds like an easy topic. Let's talk about a specific scenario - extracting content from the Excel files. If email is received with an Excel file as an attachment, we would like to get this content and process it. How to do it? Especially when there is a need to support the older file format (xls) and the new one (xlsx)? In this article, we will go through the implementation of the solution responsible for the automated extraction of the content from Excel files using Azure Logic Apps and Azure Function Apps. In the end, we are going to store extracted data in the Azure Cosmos DB.

The above diagram presents the flow:

1. An email with Excel file as an attachment is received
2. Excel file is stored on the Azure Blog Storage
3. Azure Function is triggered and Excel file is extracted
4. Extracted data is stored in the Azure Cosmos DB


# Azure Logic App implementation

We are going to start with Azure Logic App implementation. In this scenario, we want to trigger Logic App when a new email with attachment is received. Then we want to store this file on the Blob Storage to make it available for the Azure Function. Here is the flow of the Azure Logic App:

<p align="center">
<img src="assets/ExcelContentExtractionWithAzure3.PNG?raw=true" alt="Image not found"/>
</p>

Once the Excel file is received, we can extract its content using Azure Function App.



# Azure Function App implementation

We need to create Blob Trigger Function App to react always when there is a new file created on the Azure Blob Storage.

<p align="center">
<img src="assets/ExcelContentExtractionWithAzure4.PNG?raw=true" alt="Image not found"/>
</p>

## Excel file content extraction

To extract the content, we are going to use *NPOI* library available as [NuGet package](https://www.nuget.org/packages/NPOI/). With this library, we can read both - older version of the Excel file (XLS) and the new one (XLSX).


```csharp
    public class ExcelFileContentExtractorService : IExcelFileContentExtractorService
    {
        public ExcelFileRawDataModel GetFileContent(Stream fileStream)
        {
            IDictionary<string, string> cellValues = new Dictionary<string, string>();
            IWorkbook workbook = WorkbookFactory.Create(fileStream);
            ISheet reportSheet = workbook.GetSheetAt(0);

            if (reportSheet != null)
            {
                int rowCount = reportSheet.LastRowNum + 1;
                for (int i = 0; i < rowCount; i++)
                {
                    IRow row = reportSheet.GetRow(i);
                    if (row != null)
                    {
                        foreach (var cell in row.Cells)
                        {
                            var cellValue = cell.GetFormattedCellValue();
                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                cellValues.Add(cell.Address.FormatAsString(), cellValue);
                            }
                        }
                    }
                }
            }

            return new ExcelFileRawDataModel()
            {
                Id = cellValues["A6"],
                CarBrand = cellValues["B6"],
                CarModel = cellValues["C6"],
                CarPrice = decimal.Parse(cellValues["D6"]),
                CarAvailability = int.Parse(cellValues["E6"])
            };
        }
    }
```

Please note that we can extract data from specific rows and columns.


## Integration with Azure Cosmos DB

Once we got data extracted from the file, we can store in the Azure Cosmos DB (or any other database). Then we can use data in any other system. To connect with Azure Cosmos DB I used new SDK:

```csharp
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
```

Here is the sample data extracted, visible in the Azure Cosmos DB Data Explorer:

<p align="center">
<img src="assets/ExcelContentExtractionWithAzure5.PNG?raw=true" alt="Image not found"/>
</p>
