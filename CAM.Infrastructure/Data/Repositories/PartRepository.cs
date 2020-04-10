using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CAM.Infrastructure.Data.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly ILogger<PartRepository> _logger;
        private readonly ApplicationContext _applicationContext;
        public PartRepository(ILogger<PartRepository> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;
        }

        public async Task<Part> GetByIdAsync(int id, bool inclTracking = true)
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
        public async Task<Part> GetByMfrsPnAsync(string partNum, bool inclTracking = true)
        {
            Part result;
            var queryable = _applicationContext.Set<Part>()
                .Where(e => e.MfrsPartNumber == partNum)
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
        public IQueryable<Part> GetBySearchParamsAsync(string search, string filter,
        int page, int ipp)
        {
            search = search ?? String.Empty;
            filter = filter ?? String.Empty;
            IQueryable<Part> queryable = GetQueryableBySearch(search, filter).AsNoTracking();

            return queryable;
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
        public async Task<bool> CheckForExistingRecordByPnAsync(string mfrsPartNumber)
        {
            return await _applicationContext.Parts.AnyAsync(e => e.MfrsPartNumber.ToLower() == mfrsPartNumber.ToLower()) ? true : false;
        }
        public async Task<bool> CheckForExistingRecordByIdAsync(int id)
        {
            return await _applicationContext.Parts.AnyAsync(e => e.Id == id) ? true : false;
        }
        public async Task AddAsync(Part part)
        {
            _logger.LogInformation($"Attempting to save new part Id:{part.Id} Manufacturer's #:{part.MfrsPartNumber}.");
            await _applicationContext.BeginTransaction();
            await _applicationContext.AddAsync(part);
            await _applicationContext.Commit();
            _logger.LogInformation($"Successfully saved new part Id:{part.Id} Manufacturer's #:{part.MfrsPartNumber}.");
        }
        public async Task SaveChangesAsync()
        {
            await _applicationContext.BeginTransaction();
            await _applicationContext.Commit();
        }
        public async Task DeleteAsync(Part part)
        {
            _logger.LogInformation($"Attempting to soft delete part Id:{part.Id} Manufacturer's #:{part.MfrsPartNumber}.");
            await _applicationContext.BeginTransaction();
            part.SoftDelete();
            await _applicationContext.Commit();
            _logger.LogInformation($"Successfully soft deleted part Id:{part.Id} Manufacturer's #:{part.MfrsPartNumber}.");
        }
        private IQueryable<Part> GetQueryableBySearch(string search, string filter)
        {
            IQueryable<Part> queryable;
            if (!String.IsNullOrWhiteSpace(search))
            {
                queryable = _applicationContext.Set<Part>()
                    .Where(e => e.Name.ToLower().Contains(search.ToLower()) || e.Description.ToLower().Contains(search.ToLower()) ||
                        e.MfrsPartNumber.ToLower().Contains(search) || e.CataloguePartNumber.ToLower().Contains(search))
                    .Include(e => e.PartCategory)
                    .OrderBy(e => e.MfrsPartNumber.Length);
            }
            else
            {
                queryable = _applicationContext.Set<Part>()
                    .Include(e => e.PartCategory)
                    .OrderBy(e => e.MfrsPartNumber.Length);
            }

            if (!String.IsNullOrWhiteSpace(filter))
                queryable = queryable.Where(e => e.CurrentStock < e.MinimumStock);

            return queryable;
        }
    }
}