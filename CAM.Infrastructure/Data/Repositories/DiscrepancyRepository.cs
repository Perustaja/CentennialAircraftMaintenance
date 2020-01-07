using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Entities.DiscrepancyAggregate;
using CAM.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Repositories
{
    public class DiscrepancyRepository : IDiscrepancyRepository
    {
        private readonly ApplicationContext _applicationContext;

        public DiscrepancyRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<Discrepancy> GetByIdAsync(int id, bool inclTracking = true)
        {
            Discrepancy result;
            var queryable = _applicationContext.Set<Discrepancy>()
                    .Where(e => e.WorkOrderId == null && e.Id == id)
                    .Include(e => e.LaborRecords)
                    .Include(e => e.DiscrepancyParts)
                        .ThenInclude(d => d.Part)
                    .Include(e => e.WorkStatus);

            if (inclTracking)
                result = await queryable.FirstOrDefaultAsync();
            else
                result = await queryable.AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<Discrepancy>> GetListAllAsync(bool inclTracking = true)
        {
            List<Discrepancy> result;
            var queryable = _applicationContext.Set<Discrepancy>()
                    .Where(e => e.WorkOrderId == null)
                    .Include(e => e.LaborRecords)
                    .Include(e => e.DiscrepancyParts)
                        .ThenInclude(d => d.Part)
                    .Include(e => e.WorkStatus);
            
            if (inclTracking)
                result = await queryable.ToListAsync();
            else
                result = await queryable.AsNoTracking().ToListAsync();
            return result;
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