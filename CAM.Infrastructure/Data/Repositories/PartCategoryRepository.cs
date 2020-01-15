using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Repositories
{
    public class PartCategoryRepository : IPartCategoryRepository
    {
        private readonly ApplicationContext _applicationContext;

        public PartCategoryRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<List<PartCategory>> GetListAllAsync()
        {
            return await _applicationContext.Set<PartCategory>().ToListAsync();
        }
    }
}