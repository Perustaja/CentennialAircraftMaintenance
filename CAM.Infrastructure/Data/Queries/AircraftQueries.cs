using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Queries
{
    public static class AircraftQueries
    {
        public async static Task<Aircraft> GetByIdAsync(
        this DbSet<Aircraft> set, string id, bool inclTracking = true)
        {
            if (inclTracking)
                return await set.Where(e => e.Id == id)
                                .Include(e => e.Times)
                                .Include(e => e.Squawks)
                                .FirstOrDefaultAsync();
            else
                return await set.Where(e => e.Id == id)
                                .Include(e => e.Times)
                                .Include(e => e.Squawks)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
        }

        public async static Task<List<Aircraft>> GetListAllAsync(
            this DbSet<Aircraft> set, bool inclTracking = false)
        {
            if (inclTracking)
                return await set.ToListAsync();
            else
                return await set.AsNoTracking().ToListAsync();
        }

        public async static Task<List<Aircraft>> GetListAsync(
        this DbSet<Aircraft> set, string searchString, bool inclTracking = false)
        {
            if (inclTracking)
                return await set.Where(e => e.Id.Contains(searchString))
                                .ToListAsync();
            else
                return await set.Where(e => e.Id.Contains(searchString))
                                .AsNoTracking()
                                .ToListAsync();
        }
    }
}