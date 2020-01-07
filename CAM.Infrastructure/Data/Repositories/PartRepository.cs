using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly ApplicationContext _applicationContext;
        public PartRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<List<Part>> GetListAllAsync(bool inclTracking = true)
        {
            if (inclTracking)
                return await _applicationContext.Set<Part>()
                    .Include(e => e.PartCategory)
                    .ToListAsync();
            else
                return await _applicationContext.Set<Part>()
                    .Include(e => e.PartCategory)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<List<Part>> GetBySearchParamsAsync(string search, string filter, bool inclTracking = true)
        {
            search = search ?? "";
            List<Part> result;
            IQueryable<Part> queryable;
            // A little messy, but we search first regardless and then apply the specific filter if needed.
            // A more elegant solution will be required for any serious filtering.
            queryable = _applicationContext.Set<Part>()
                .Where(e => e.Name.ToLower().Contains(search.ToLower()) || e.Description.ToLower().Contains(search.ToLower()))
                .Include(e => e.PartCategory);
            
            if (!String.IsNullOrEmpty(filter))
                queryable = queryable.Where(e => e.CurrentStock < e.MinimumStock);
            
            if (inclTracking)
                result = await queryable.ToListAsync();
            else
                result = await queryable.AsNoTracking().ToListAsync();
            return result;
        }
    }
}