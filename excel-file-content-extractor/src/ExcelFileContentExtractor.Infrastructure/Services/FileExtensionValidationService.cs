using ExcelFileContentExtractor.Infrastructure.Enums;
using ExcelFileContentExtractor.Infrastructure.Services.Interfaces;
using System;
using System.IO;

namespace ExcelFileContentExtractor.Infrastructure.Services
{
    public class FileExtensionValidationService : IFileExtensionValidationService
    {
        public bool IsValidExtension(Uri filePath)
        {
            string fileExtension = Path.GetExtension(filePath.ToString());

            if (fileExtension.Equals($".{SupportedFileTypes.XLSX}", StringComparison.InvariantCultureIgnoreCase)
                ||
                fileExtension.Equals($".{SupportedFileTypes.XLS}", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
