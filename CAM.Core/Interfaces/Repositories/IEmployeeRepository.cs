using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;

namespace CAM.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetListAll();
        Task<bool> EmployeeExists(int employeeId);
    }
}