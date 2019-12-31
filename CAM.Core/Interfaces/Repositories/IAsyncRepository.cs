using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.SharedKernel;

namespace CAM.Core.Interfaces.Repositories
{
    /// <summary>
    /// Includes only generic methods that will not have related data. 
    /// </summary>
    public interface IAsyncRepository<T, U> where T : BaseEntity<U> 
    {
        Task<List<T>> GenericListAllAsync(bool inclTracking = true);
    }
}