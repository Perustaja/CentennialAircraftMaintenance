using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.SharedKernel;

namespace CAM.Core.Interfaces.Repositories
{
    public interface IPartRepository
    {
        Task<Part> GetByIdAsync(string id, bool inclTracking = true);
        Task<List<Part>> GetListAllAsync(bool inclTracking = true);
        Task<PaginatedList<Part>> GetBySearchParamsAsync(string search, string filter, int page, int ipp, bool inclTracking = true);
        Task<List<Part>> GetByApiSearchValues(string search);
        Task<bool> CheckForExistingRecordAsync(string id);
        Task AddAsync(Part part);
        Task DeleteAsync(Part part);
        Task SaveChangesAsync();
    }
}