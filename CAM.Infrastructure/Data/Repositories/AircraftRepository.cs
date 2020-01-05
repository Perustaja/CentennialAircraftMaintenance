using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Repositories
{
    public class AircraftRepository : IAircraftRepository
    {
        private readonly ApplicationContext _applicationContext;

        public AircraftRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<Aircraft> GetByIdAsync(string id, bool inclTracking = true)
        {
            if (inclTracking)
                return await _applicationContext.Set<Aircraft>()
                    .Where(e => e.Id == id)
                    .Include(e => e.Times)
                    .Include(e => e.Squawks)
                    .FirstOrDefaultAsync();
            else
                return await _applicationContext.Set<Aircraft>()
                    .Where(e => e.Id == id)
                    .Include(e => e.Times)
                    .Include(e => e.Squawks)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Aircraft>> GetListAllAsync(bool inclTracking = true)
        {
            if (inclTracking)
                return await _applicationContext.Set<Aircraft>()
                    .ToListAsync();
            else
                return await _applicationContext.Set<Aircraft>()
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<List<Aircraft>> GetListAsync(string searchString, bool inclTracking = true)
        {
            if (inclTracking)
                return await _applicationContext.Set<Aircraft>()
                    .Where(e => e.Id.Contains(searchString))
                    .ToListAsync();
            else
                return await _applicationContext.Set<Aircraft>()
                    .Where(e => e.Id.Contains(searchString))
                    .AsNoTracking()
                    .ToListAsync();
        }
    }
}