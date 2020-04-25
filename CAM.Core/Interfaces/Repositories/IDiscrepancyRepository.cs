using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;

namespace CAM.Core.Interfaces.Repositories
{
    public interface IDiscrepancyRepository
    {
        /// <param name="index">The one-based index.</param>
        Task<Discrepancy> GetByWorkOrderAndIndexOrDefault(int wOrderId, int index, bool inclTracking = true);
        Task<List<DiscrepancyPart>> GetDiscrepancyPartsById(int discrepId, bool inclTracking);
        Task<List<LaborRecord>> GetLaborRecordsById(int discrepId, bool inclTracking);
        Task<bool> DiscrepancyExists(int discrepId);
        Task AddDiscrepancyPart(int discrepId, int partId, int qty);
        Task AddLaborRecord(int discrepId, int employeeId, decimal hours, DateTime date);
    }
}