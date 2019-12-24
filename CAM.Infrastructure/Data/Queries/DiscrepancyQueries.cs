using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Queries
{
    public static class DiscrepancyQueries
    {
        public async static Task<List<Discrepancy>> GetBySearchParams(
            this DbSet<Discrepancy> set, string regNum, string status)
        {
            return await set.
            Where(d => d.AircraftId.Contains(regNum) && d.WorkStatus.Description.ToLower() == status)
            .AsNoTracking()
            .ToListAsync();
        }
    }
}