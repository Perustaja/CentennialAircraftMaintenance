using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Interfaces.Repositories;
using CAM.Core.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Only contains easily genericized methods that don't involve loading relational data as that's hard 
    /// to genericize.
    /// </summary>

    public class BaseEfRepository<T, U> : IAsyncRepository<T, U> where T : BaseEntity<U>
    {
        protected readonly ApplicationContext _applicationContext;
        public BaseEfRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<List<T>> GenericListAllAsync(bool inclTracking = true)
        {
            if (inclTracking)
                return await _applicationContext.Set<T>().ToListAsync();
            else
                return await _applicationContext.Set<T>().AsNoTracking().ToListAsync();
        }
    }
}