using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;

namespace CAM.Core.Interfaces.Repositories
{
    public interface IWorkOrderRepository
    {
        Task<WorkOrder> GetById(int id, bool inclTracking = true);
        Task<List<WorkOrder>> GetListAll(bool inclTracking = true);
        Task<List<WorkOrder>> GetBySearchParams(string regNum, string status);
    }
}