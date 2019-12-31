using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Entities.DiscrepancyAggregate;
using CAM.Core.SharedKernel;

namespace CAM.Core.Interfaces.Repositories
{
    public interface IWorkOrderRepository
    {
        Task<WorkOrder> GetByIdAsync(int id, bool inclTracking = true);
        Task<List<WorkOrder>> GetListAllAsync(bool inclTracking = true);
        Task<List<WorkOrder>> GetBySearchParamsAsync(string regNum, string status);
    }
}