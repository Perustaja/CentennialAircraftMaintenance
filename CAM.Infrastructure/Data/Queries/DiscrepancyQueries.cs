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
        public async static Task<> GetByIdAsync(
            this DbSet<Discrepancy> set, int id, bool inclTracking = false)
        {
            if (inclTracking)
                return await set.Where(e => e.Id.Equals(id)).FirstOrDefaultAsync();
            else
                return await set.Where(e => e.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();

        }
        public async static Task<List<T>> GetListAllAsync<T>(
            this DbSet<T> set, bool inclTracking = false)
            where T : class
        {
            if (inclTracking)
                return await set.ToListAsync();
            else
                return await set.AsNoTracking().ToListAsync();
        }
        public async static Task<List<T>> GetListAsync<T>(
            this DbSet<T> set, string exp, bool inclTracking = false)
            where T : BaseEntity<string>
        {
            if (inclTracking)
                return await set.Where(e => e.Id.Contains(exp)).ToListAsync();
            else
                return await set.Where(e => e.Id.Contains(exp)).AsNoTracking().ToListAsync();
        }
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