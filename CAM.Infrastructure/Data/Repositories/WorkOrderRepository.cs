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
            WorkOrder result;
            var queryable = _applicationContext.Set<WorkOrder>()
                .Where(e => e.Id == id);

            if (inclTracking)
                result = await queryable.FirstOrDefaultAsync();
            else
                result = await queryable.AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<WorkOrder>> GetListAllAsync(bool inclTracking = true)
        {
            List<WorkOrder> result;
            var queryable = _applicationContext.Set<WorkOrder>()
                .Include(e => e.Discrepancies)
                .Include(e => e.WorkStatus);
            if (inclTracking)
                result = await queryable.AsNoTracking().ToListAsync();
            else
                result = await queryable.ToListAsync();
            return result;
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