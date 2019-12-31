using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.SharedKernel;

namespace CAM.Core.Interfaces.Repositories
{
    public interface IAircraftRepository
    {
        Task<Aircraft> GetByIdAsync(string id, bool inclTracking = true);

        Task<List<Aircraft>> GetListAllAsync(bool inclTracking = true);

        Task<List<Aircraft>> GetListAsync(string searchString, bool inclTracking = true);
    }
}