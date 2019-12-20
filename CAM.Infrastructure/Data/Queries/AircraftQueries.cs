using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Queries
{
    public static class AircraftQueries
    {
        public async static Task<List<Discrepancy>> GetDiscrepanciesByAircraftId(
            this DbSet<Discrepancy> set, string id, bool inclTracking = false)
        {
            if (inclTracking)
                return await set.Where(d => d.AircraftId == id).ToListAsync();
            else
                return await set.Where(d => d.AircraftId == id).AsNoTracking().ToListAsync();
        }
    }
}