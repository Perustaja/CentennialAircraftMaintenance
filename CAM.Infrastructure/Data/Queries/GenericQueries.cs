using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Queries
{
    public static class GenericQueries
    {
        public async static Task<List<T>> GenericListAllAsync<T>(
            this DbSet<T> set, bool inclTracking = false) where T : BaseEntity<int>
        {
            if (inclTracking)
                return await set.ToListAsync();
            else
                return await set.AsNoTracking().ToListAsync();
        }
    }
}