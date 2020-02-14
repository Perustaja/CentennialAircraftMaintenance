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

        public async Task<Part> GetByIdAsync(string id, bool inclTracking = true)
        {
            Part result;
            var queryable = _applicationContext.Set<Part>().Where(e => e.Id == id)
                .Include(e => e.PartCategory);

            if (inclTracking)
                result = await queryable.FirstOrDefaultAsync();
            else
                result = await queryable.AsNoTracking().FirstOrDefaultAsync();
            return result;
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
                .Where(e => e.Name.ToLower().Contains(search.ToLower()) || e.Description.ToLower().Contains(search.ToLower()) ||
                    e.Id.ToLower().Contains(search) || e.CataloguePartNumber.ToLower().Contains(search))
                .Include(e => e.PartCategory);

            if (!String.IsNullOrEmpty(filter))
                queryable = queryable.Where(e => e.CurrentStock < e.MinimumStock && (!e.IsDiscontinued));

            if (inclTracking)
                result = await queryable.ToListAsync();
            else
                result = await queryable.AsNoTracking().ToListAsync();
            return result;
        }
        /// <summary>
        /// Checks if a part with a matching id exists. Both strings are copied with ToLower() so it is not case-sensitive.
        /// </summary> 
        public async Task<bool> CheckForExistingRecordAsync(string id)
        {
            return await _applicationContext.Parts.AnyAsync(e => e.Id.ToLower() == id.ToLower()) ? true : false;
        }


        public async Task AddAsync(Part part)
        {
            await _applicationContext.BeginTransaction();
            await _applicationContext.AddAsync(part);
            await _applicationContext.Commit();
        }

        public async Task SaveChangesAsync()
        {
            await _applicationContext.BeginTransaction();
            await _applicationContext.Commit();
        }


        public async Task DeleteAsync(Part part)
        {
            await _applicationContext.BeginTransaction();
            _applicationContext.Parts.Remove(part);
            await _applicationContext.Commit();
        }
    }
}