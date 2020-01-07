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
            Aircraft result;
            var queryable = _applicationContext.Set<Aircraft>()
                .Include(e => e.Times)
                .Include(e => e.Squawks);

            if (inclTracking)
                result = await queryable.FirstOrDefaultAsync();
            else
                result = await queryable.AsNoTracking().FirstOrDefaultAsync();
            return result;
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
            List<Aircraft> result;
            var queryable = _applicationContext.Set<Aircraft>()
                .Where(e => e.Id.Contains(searchString));

            if (inclTracking)
                result = await queryable.ToListAsync();
            else
                result = await queryable.AsNoTracking().ToListAsync();
            return result;
        }
    }
}