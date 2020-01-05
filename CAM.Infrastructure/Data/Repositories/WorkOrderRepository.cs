using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Entities.DiscrepancyAggregate;
using CAM.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Repositories
{
    public class WorkOrderRepository : IWorkOrderRepository
    {
        private readonly ApplicationContext _applicationContext;

        public WorkOrderRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        
        public async Task<WorkOrder> GetByIdAsync(int id, bool inclTracking = false)
        {
            if (inclTracking)
                return await _applicationContext.Set<WorkOrder>()
                    .Where(e => e.Id == id)
                    .FirstOrDefaultAsync();
            else
                return await _applicationContext.Set<WorkOrder>()
                .Where(e => e.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<WorkOrder>> GetListAllAsync(bool inclTracking = true)
        {
            if (inclTracking)
                return await _applicationContext.Set<WorkOrder>()
                    .Include(e => e.Discrepancies)
                    .Include(e => e.WorkStatus)
                    .ToListAsync();
            else
                return await _applicationContext.Set<WorkOrder>()
                    .Include(e => e.Discrepancies)
                    .Include(e => e.WorkStatus)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<List<WorkOrder>> GetBySearchParamsAsync(string regNum, string status)
        {
            return await _applicationContext.Set<WorkOrder>()
                .Where(e => e.AircraftId.Contains(regNum) && e.WorkStatus.Description.ToLower() == status)
                .Include(e => e.WorkStatus)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}