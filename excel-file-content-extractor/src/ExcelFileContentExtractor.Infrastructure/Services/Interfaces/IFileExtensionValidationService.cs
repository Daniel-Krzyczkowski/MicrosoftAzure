using System;

namespace ExcelFileContentExtractor.Infrastructure.Services.Interfaces
{
    public interface IFileExtensionValidationService
    {
        bool IsValidExtension(Uri filePath);
    }
}
