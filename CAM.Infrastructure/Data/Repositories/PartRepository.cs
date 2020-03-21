using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Interfaces.Repositories;
using CAM.Core.SharedKernel;
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
        /// <summary>
        /// Returns a sorted and filtered PaginatedList of Parts ordered by their id length.
        /// </summary> 
        public async Task<PaginatedList<Part>> GetBySearchParamsAsync(string search, string filter, 
        int page, int ipp, bool inclTracking = true)
        {
            search = search ?? String.Empty;
            filter = filter ?? String.Empty;
            PaginatedList<Part> result;
            IQueryable<Part> queryable = GetQueryableBySearch(search, filter);

            if (inclTracking)
                result = await PaginatedList<Part>.CreateAsync(queryable, page, ipp);
            else
                result = await PaginatedList<Part>.CreateAsync(queryable.AsNoTracking(), page, ipp);
            return result;
        }
        /// <summary>
        /// Gets by API search values ordered by their id length. Returns an empty list if argument is null.
        /// </summary> 
        public async Task<List<Part>> GetByApiSearchValues(string search)
        {
            if (String.IsNullOrWhiteSpace(search))
                return new List<Part>(); // Return an empty list
            
            IQueryable<Part> queryable = GetQueryableBySearch(search, String.Empty);

            return await queryable.AsNoTracking().ToListAsync();
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
        private IQueryable<Part> GetQueryableBySearch(string search, string filter)
        {
            IQueryable<Part> queryable;
            if (!String.IsNullOrWhiteSpace(search))
            {
                queryable = _applicationContext.Set<Part>()
                    .Where(e => e.Name.ToLower().Contains(search.ToLower()) || e.Description.ToLower().Contains(search.ToLower()) ||
                        e.Id.ToLower().Contains(search) || e.CataloguePartNumber.ToLower().Contains(search))
                    .Include(e => e.PartCategory)
                    .OrderBy(e => e.Id.Length);
            }
            else
            {
                queryable = _applicationContext.Set<Part>()
                    .Include(e => e.PartCategory)
                    .OrderBy(e => e.Id.Length);
            }

            if (!String.IsNullOrWhiteSpace(filter))
                queryable = queryable.Where(e => e.CurrentStock < e.MinimumStock && (!e.IsDiscontinued));

            return queryable;
        }
    }
}