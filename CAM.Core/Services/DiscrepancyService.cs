using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Interfaces.Repositories;
using CAM.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace CAM.Core.Services
{
    public class DiscrepancyService : IDiscrepancyService
    {
        private readonly ILogger<DiscrepancyService> _logger;
        private readonly IDiscrepancyRepository _discrepRepo;
        private readonly IPartRepository _partRepo;
        private readonly IEmployeeRepository _employeeRepo;
        public DiscrepancyService(ILogger<DiscrepancyService> logger, IDiscrepancyRepository discrepRepo,
        IPartRepository partRepo, IEmployeeRepository empRepo)
        {
            _logger = logger;
            _discrepRepo = discrepRepo;
            _partRepo = partRepo;
            _employeeRepo = empRepo;
        }
        public async Task<Discrepancy> GetDiscrepOrDefaultByIndex(int wOrderId, int index, bool inclTracking)
        {
            return await _discrepRepo.GetByWorkOrderAndIndexOrDefault(wOrderId, index, inclTracking);
        }
        public async Task<List<DiscrepancyPart>> GetDiscrepancyPartsById(int discrepId, bool inclTracking = true)
        {
            return await _discrepRepo.GetDiscrepancyPartsById(discrepId, inclTracking);
        }
        public async Task<List<LaborRecord>> GetLaborRecordsById(int discrepId, bool inclTracking = true)
        {
            return await _discrepRepo.GetLaborRecordsById(discrepId, inclTracking);
        }

        public async Task<bool> TryAddPart(int discrepId, int partId, int qty)
        {
            var partExists = await _partRepo.PartExistsById(partId);
            var discExists = await _discrepRepo.DiscrepancyExists(discrepId);
            if (partExists && discExists)
            {
                try
                {
                    await _discrepRepo.AddDiscrepancyPart(discrepId, partId, qty);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
        public async Task<bool> TryAddLabor(int discrepId, int employeeId, decimal hours, DateTime date)
        {
            var discrepExists = await _discrepRepo.DiscrepancyExists(discrepId);
            var employeeExists = await _employeeRepo.EmployeeExists(employeeId);
            if (discrepExists && employeeExists)
            {
                try
                {
                    await _discrepRepo.AddLaborRecord(discrepId, employeeId, hours, date);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}