using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Repositories
{
    public class WorkStatusRepository : IWorkStatusRepository
    {
        private readonly ApplicationContext _applicationContext;

        public WorkStatusRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<WorkStatus>> GetListAllAsync(bool inclTracking = true)
        {
            if (inclTracking)
                return await _applicationContext.Set<WorkStatus>()
                    .ToListAsync();
            else
                return await _applicationContext.Set<WorkStatus>()
                    .AsNoTracking()
                    .ToListAsync();
        }
    }
}