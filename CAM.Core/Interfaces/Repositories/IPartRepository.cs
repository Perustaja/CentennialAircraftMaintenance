using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;

namespace CAM.Core.Interfaces.Repositories
{
    public interface IPartRepository
    {
        Task<Part> GetByIdAsync(int id, bool inclTracking = true);
        Task<Part> GetByMfrsPnAsync(string partNum, bool inclTracking = true);
        Task<List<Part>> GetListAllAsync(bool inclTracking = true);
        IQueryable<Part> GetBySearchParamsAsync(string search, string filter, int page, int ipp);
        Task<List<Part>> GetByApiSearchValues(string search);
        Task<bool> CheckForExistingRecordByPnAsync(string mfrsPartNumber);
        Task<bool> CheckForExistingRecordByIdAsync(int id);
        Task AddAsync(Part part);
        Task DeleteAsync(Part part);
        Task SaveChangesAsync();
    }
}