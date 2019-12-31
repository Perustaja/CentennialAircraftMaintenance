using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Entities.DiscrepancyAggregate;
using CAM.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Repositories
{
    public class DiscrepancyRepository : BaseEfRepository<Discrepancy, int>, IDiscrepancyRepository
    {
        public DiscrepancyRepository(ApplicationContext applicationContext) : base(applicationContext) {}
        public async Task<Discrepancy> GetByIdAsync(int id, bool inclTracking = true)
        {
            if (inclTracking)
            return await _applicationContext.Set<Discrepancy>()
                .Where(e => e.WorkOrderId == null && e.Id == id)
                .Include(e => e.LaborRecords)
                .Include(e => e.DiscrepancyParts)
                    .ThenInclude(d => d.Part)
                .Include(e => e.WorkStatus)
                .FirstOrDefaultAsync();
            else
            return await _applicationContext.Set<Discrepancy>()
                .Where(e => e.WorkOrderId == null && e.Id == id)
                .Include(e => e.LaborRecords)
                .Include(e => e.DiscrepancyParts)
                    .ThenInclude(d => d.Part)
                .Include(e => e.WorkStatus)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<Discrepancy>> GetListAllAsync(bool inclTracking = true)
        {
            if (inclTracking)
            return await _applicationContext.Set<Discrepancy>()
                .Where(e => e.WorkOrderId == null)
                .Include(e => e.LaborRecords)
                .Include(e => e.DiscrepancyParts)
                    .ThenInclude(d => d.Part)
                .Include(e => e.WorkStatus)
                .ToListAsync();
            else
            return await _applicationContext.Set<Discrepancy>()
                .Where(e => e.WorkOrderId == null)
                .Include(e => e.LaborRecords)
                .Include(e => e.DiscrepancyParts)
                    .ThenInclude(d => d.Part)
                .Include(e => e.WorkStatus)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Discrepancy>> GetBySearchParamsAsync(string regNum, string status)
        {
            return await _applicationContext.Set<Discrepancy>()
                .Where(e => e.WorkOrderId == null)
                .Where(e => e.AircraftId.Contains(regNum) && e.WorkStatus.Description.ToLower() == status)
                .Include(e => e.WorkStatus)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}