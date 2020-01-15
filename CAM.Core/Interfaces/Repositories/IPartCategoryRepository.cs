using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;

namespace CAM.Core.Interfaces.Repositories
{
    public interface IPartCategoryRepository
    {
        Task<List<PartCategory>> GetListAllAsync();
    }
}