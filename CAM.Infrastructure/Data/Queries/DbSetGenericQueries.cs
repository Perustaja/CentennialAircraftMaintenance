using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Interfaces;
using CAM.Core.SharedKernel;
using Microsoft.EntityFrameworkCore;


namespace CAM.Infrastructure.Data.DbSetExtensions
{
    public static class DbSetGenericQueries
    {
        public async static Task<T> GetByIdAsync<T, U>(this DbSet<T> set, U id)
        where T : BaseEntity<U>
        {
            return await set.Where(e => e.Id.Equals(id)).FirstOrDefaultAsync();
        }
        
    }
}