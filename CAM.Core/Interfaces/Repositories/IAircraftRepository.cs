using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;

namespace CAM.Core.Interfaces.Repositories
{
    public interface IAircraftRepository
    {
        Task<Aircraft> GetById(string id, bool inclTracking = true);

        Task<List<Aircraft>> GetListAll(bool inclTracking = true);

        Task<List<Aircraft>> GetList(string searchString, bool inclTracking = true);
    }
}