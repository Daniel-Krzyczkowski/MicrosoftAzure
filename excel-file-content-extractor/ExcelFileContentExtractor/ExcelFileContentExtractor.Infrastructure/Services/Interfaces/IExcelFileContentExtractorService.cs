using ExcelFileContentExtractor.Core.Model;
using System.IO;

namespace ExcelFileContentExtractor.Infrastructure.Services.Interfaces
{
    public interface IExcelFileContentExtractorService
    {
        ExcelFileRawDataModel GetFileContent(Stream fileStream);
    }
}
