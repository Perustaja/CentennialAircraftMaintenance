using System.Threading.Tasks;

namespace CAM.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}