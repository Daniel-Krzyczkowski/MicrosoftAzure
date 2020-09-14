using ExcelFileContentExtractor.Infrastructure.Enums;
using ExcelFileContentExtractor.Infrastructure.Services.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExcelFileContentExtractor.FunctionApp
{
    public class ExcelFileContentExtractorFuncApp
    {
        private readonly IFileExtensionValidationService _fileExtensionValidationService;
        private readonly IExcelFileContentExtractorService _excelFileContentExtractorService;

        public ExcelFileContentExtractorFuncApp(IFileExtensionValidationService fileExtensionValidationService,
                                                IExcelFileContentExtractorService excelFileContentExtractorService)
        {
            _fileExtensionValidationService = fileExtensionValidationService;
            _excelFileContentExtractorService = excelFileContentExtractorService;
        }

        [FunctionName("excel-file-content-extractor-func-app")]
        public async Task Run([BlobTrigger("excel-files/{name}", Connection = "")] Stream excelFile, string name, Uri uri, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {excelFile.Length} Bytes");

            var isValidFileExtension = _fileExtensionValidationService.IsValidExtension(uri);
            if (isValidFileExtension)
            {
                var excelFileContent = _excelFileContentExtractorService.GetFileContent(excelFile);
            }

            else
            {
                log.LogError($"Extension of uploaded file {name} is not supported. Supported extensions are: {SupportedFileTypes.XLS} and {SupportedFileTypes.XLSX}");
            }
        }
    }
}
