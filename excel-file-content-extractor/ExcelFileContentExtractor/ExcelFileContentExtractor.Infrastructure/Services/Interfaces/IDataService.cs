using ExcelFileContentExtractor.Core.Model;
using System.Threading.Tasks;

namespace ExcelFileContentExtractor.Infrastructure.Services.Interfaces
{
    public interface IDataService<T> where T : class, IEntity
    {
        Task<T> AddAsync(T newEntity);
    }
}
