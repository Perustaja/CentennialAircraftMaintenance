using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Entities.DiscrepancyAggregate;
using CAM.Core.SharedKernel;

namespace CAM.Core.Interfaces.Repositories
{
    public interface IDiscrepancyRepository
    {
        Task<Discrepancy> GetByIdAsync(int id, bool inclTracking = true);
        Task<List<Discrepancy>> GetListAllAsync(bool inclTracking = true);
        Task<List<Discrepancy>> GetBySearchParamsAsync(string regNum, string status);
    }
}