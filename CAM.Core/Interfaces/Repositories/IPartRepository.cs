using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;

namespace CAM.Core.Interfaces.Repositories
{
    public interface IPartRepository
    {
        Task<Part> GetByIdOrDefault(int id, bool inclTracking = true);
        Task<Part> GetByMfrsPnOrDefault(string partNum, bool inclTracking = true);
        Task<List<Part>> GetListAll(bool inclTracking = true);
        IQueryable<Part> GetBySearchParams(string search, string filter, int page, int ipp);
        Task<List<Part>> GetByApiSearchValues(string search);
        Task<bool> PartExistsByPartNumber(string mfrsPartNumber);
        Task<bool> PartExistsById(int id);
        Task Add(Part part);
        Task Delete(Part part);
        Task SaveChanges();
    }
}