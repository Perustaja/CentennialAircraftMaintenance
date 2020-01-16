using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;

namespace CAM.Core.Interfaces.Repositories
{
    public interface IPartRepository
    {
        Task<Part> GetByIdAsync(string id, bool inclTracking = true);
        Task<List<Part>> GetListAllAsync(bool inclTracking = true);
        Task<List<Part>> GetBySearchParamsAsync(string search, string filter, bool inclTracking = true);
        Task AddAsync(Part part);
    }
}