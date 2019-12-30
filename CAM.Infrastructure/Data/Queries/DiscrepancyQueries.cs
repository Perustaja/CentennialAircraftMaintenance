using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities.DiscrepancyAggregate;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Queries
{
    public static class DiscrepancyQueries
    {
        public async static Task<Discrepancy> GetByIdAsync(
            this DbSet<Discrepancy> set, int id, bool inclTracking = false)
        {
            if (inclTracking)
                return await set.Where(e => e.WorkOrderId == null && e.Id == id)
                                .Include(e => e.LaborRecords)
                                .Include(e => e.DiscrepancyParts)
                                    .ThenInclude(d => d.Part)
                                .Include(e => e.WorkStatus)
                                .FirstOrDefaultAsync();
            else
                return await set.Where(e => e.WorkOrderId == null && e.Id == id)
                                .Include(e => e.LaborRecords)
                                .Include(e => e.DiscrepancyParts)
                                    .ThenInclude(d => d.Part)
                                .Include(e => e.WorkStatus)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
        }

        public async static Task<List<Discrepancy>> GetListAllAsync(
            this DbSet<Discrepancy> set, bool inclTracking = false)
        {
            if (inclTracking)
                return await set.Where(e => e.WorkOrderId == null)
                                .Include(e => e.LaborRecords)
                                .Include(e => e.DiscrepancyParts)
                                    .ThenInclude(d => d.Part)
                                .Include(e => e.WorkStatus)
                                .ToListAsync();
            else
                return await set.Where(e => e.WorkOrderId == null)
                                .Include(e => e.LaborRecords)
                                .Include(e => e.DiscrepancyParts)
                                    .ThenInclude(d => d.Part)
                                .Include(e => e.WorkStatus)
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async static Task<List<Discrepancy>> GetBySearchParams(
            this DbSet<Discrepancy> set, string regNum, string status)
        {
            return await set
                        .Where(e => e.WorkOrderId == null)
                        .Where(e => e.AircraftId.Contains(regNum) && e.WorkStatus.Description.ToLower() == status)
                        .Include(e => e.WorkStatus)
                        .AsNoTracking()
                        .ToListAsync();
        }
    }
}