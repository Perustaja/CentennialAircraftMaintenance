using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;

namespace CAM.Core.Interfaces.Services
{
    public interface IDiscrepancyService
    {
        /// <param name="index">The one-based index.</param>
        Task<Discrepancy> GetDiscrepOrDefaultByIndex(int wOrderId, int index, bool inclTracking = true);
        Task<List<DiscrepancyPart>> GetDiscrepancyPartsById(int discrepId, bool inclTracking = true);
        Task<List<LaborRecord>> GetLaborRecordsById(int discrepId, bool inclTracking = true);
        Task<bool> TryAddPart(int discrepId, int partId, int qty);
        Task<bool> TryAddLabor(int discrepId, int employeeId, decimal hours, DateTime date);
    }
}