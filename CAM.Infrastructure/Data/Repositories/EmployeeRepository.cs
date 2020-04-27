using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAM.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext _applicationContext;

        public EmployeeRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<List<Employee>> GetListAll()
        {
            return await _applicationContext.Employees.ToListAsync();
        }
        public async Task<bool> EmployeeExists(int employeeId)
        {
            return await _applicationContext.Employees.AnyAsync(e => e.Id == employeeId);
        }
    }
}