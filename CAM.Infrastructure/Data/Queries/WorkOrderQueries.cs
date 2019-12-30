using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Queries
{
    public static class WorkOrderQueries
    {
        public async static Task<WorkOrder> GetByIdAsync(
            this DbSet<WorkOrder> set, int id, bool inclTracking = false)
        {
            if (inclTracking)
                return await set.Where(e => e.Id == id)
                                .FirstOrDefaultAsync();
            else
                return await set.Where(e => e.Id == id)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
        }

        public async static Task<List<WorkOrder>> GetListAllAsync(
            this DbSet<WorkOrder> set, bool inclTracking = false)
        {
            if (inclTracking)
                return await set
                                .Include(e => e.Discrepancies)
                                .Include(e => e.WorkStatus)
                                .ToListAsync();
            else
                return await set
                                .Include(e => e.Discrepancies)
                                .Include(e => e.WorkStatus)
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async static Task<List<WorkOrder>> GetBySearchParams(
            this DbSet<WorkOrder> set, string regNum, string status)
        {
            return await set
                        .Where(e => e.AircraftId.Contains(regNum) && e.WorkStatus.Description.ToLower() == status)
                        .Include(e => e.WorkStatus)
                        .AsNoTracking()
                        .ToListAsync();
        }
    }
}