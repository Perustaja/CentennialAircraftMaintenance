using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Interfaces;
using CAM.Core.SharedKernel;
using Microsoft.EntityFrameworkCore;


namespace CAM.Infrastructure.Data.Queries
{
    public static class DbSetGenericQueries
    {
        /// <summary>
        /// Performs a lookup, returning the first element with an id matching the given id, or default.
        /// </summary>
        public async static Task<T> GetByIdAsync<T, U>(
            this DbSet<T> set, U id, bool inclTracking = false)
            where T : BaseEntity<U>
        {
            if (inclTracking)
                return await set.Where(e => e.Id.Equals(id)).FirstOrDefaultAsync();
            else
                return await set.Where(e => e.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();

        }
        /// <summary>
        /// Returns all of the entities from the DbSet as a List.
        /// </summary>
        public async static Task<List<T>> GetListAllAsync<T>(
            this DbSet<T> set, bool inclTracking = false)
            where T : BaseEntity<string>
        {
            if (inclTracking)
                return await set.ToListAsync();
            else
                return await set.AsNoTracking().ToListAsync();
        }
        /// <summary>
        /// Performs a lookup, returning the first element with an id containing the given string, or default.
        /// </summary>
        public async static Task<List<T>> GetListAsync<T>(
            this DbSet<T> set, string exp, bool inclTracking = false)
            where T : BaseEntity<string>
        {
            if (inclTracking)
                return await set.Where(e => e.Id.Contains(exp)).ToListAsync();
            else
                return await set.Where(e => e.Id.Contains(exp)).AsNoTracking().ToListAsync();
        }


    }
}