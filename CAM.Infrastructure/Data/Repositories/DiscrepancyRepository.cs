using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CAM.Infrastructure.Data.Repositories
{
    public class DiscrepancyRepository : IDiscrepancyRepository
    {
        private readonly ILogger<DiscrepancyRepository> _logger;
        private readonly ApplicationContext _applicationContext;
        public DiscrepancyRepository(ILogger<DiscrepancyRepository> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;
        }
        public async Task<Discrepancy> GetByWorkOrderAndIndexOrDefault(int wOrderId, int index, bool inclTracking = true)
        {
            var queryable = _applicationContext.Discrepancies
                .Where(e => e.WorkOrderId == wOrderId)
                .Include(e => e.LaborRecords)
                    .ThenInclude(e => e.Employee)
                .Include(e => e.DiscrepancyParts)
                    .ThenInclude(e => e.Part);
            if (inclTracking)
                return await queryable.Skip(index - 1).FirstOrDefaultAsync();
            else
                return await queryable.AsNoTracking().Skip(index - 1).FirstOrDefaultAsync();
        }
        public async Task<bool> DiscrepancyExists(int discrepId)
        {
            return await _applicationContext.Discrepancies.AnyAsync(e => e.Id == discrepId);
        }
        public async Task<List<DiscrepancyPart>> GetDiscrepancyPartsById(int discrepId, bool inclTracking)
        {
            var queryable = _applicationContext.Set<DiscrepancyPart>()
                .Where(e => e.DiscrepancyId == discrepId)
                .Include(e => e.Part);
            if (inclTracking)
                return await queryable.ToListAsync();
            else
                return await queryable.AsNoTracking().ToListAsync();
        }
        public async Task<List<LaborRecord>> GetLaborRecordsById(int discrepId, bool inclTracking)
        {
            var queryable =  _applicationContext.Set<LaborRecord>()
                .Where(e => e.DiscrepancyId == discrepId);
            
            if (inclTracking)
                return await queryable.ToListAsync();
            else
                return await queryable.AsNoTracking().ToListAsync();
        }
        public async Task AddDiscrepancyPart(int discrepId, int partId, int qty)
        {
            var potentialDuplicate = await _applicationContext.Set<DiscrepancyPart>()
                .Where(e => e.DiscrepancyId == discrepId && e.PartId == partId).FirstOrDefaultAsync();

            if (potentialDuplicate != null)
            {
                _logger.LogInformation($"Attempting to add quantity to DiscrepancyPart: DiscrepancyId: {discrepId} PartId: {partId} Qty: {qty}");
                potentialDuplicate.AddQuantity(qty);
                await _applicationContext.SaveChangesAsync();
                _logger.LogInformation($"Successfully added quantity to DiscrepancyPart: DiscrepancyId: {discrepId} PartId: {partId} Qty: {qty}");
            }
            else
            {
                _logger.LogInformation($"Attempting to save new DiscrepancyPart: DiscrepancyId: {discrepId} PartId: {partId} Qty: {qty}");
                await _applicationContext.Set<DiscrepancyPart>()
                    .AddAsync(new DiscrepancyPart(discrepId, partId, qty));
                await _applicationContext.SaveChangesAsync();
                _logger.LogInformation($"Successfuly saved new DiscrepancyPart: DiscrepancyId: {discrepId} PartId: {partId} Qty: {qty}");
            }
        }

        public async Task AddLaborRecord(int discrepId, int employeeId, decimal hours, DateTime date)
        {
            var potentialDuplicate = await _applicationContext.Set<LaborRecord>()
                .Where(e => e.DiscrepancyId == discrepId && e.EmployeeId == employeeId && e.DatePerformed == date.Date).FirstOrDefaultAsync();

            if (potentialDuplicate != null)
            {
                _logger.LogInformation($"Attempting to add hours to LaborRecord.");
                potentialDuplicate.ChangeLaborHours(hours);
                await _applicationContext.SaveChangesAsync();
                _logger.LogInformation($"Successfully added hours to LaborRecord.");
            }
            else
            {
                _logger.LogInformation($"Attempting to save new LaborRecord. DiscrepancyId: {discrepId}.");
                await _applicationContext.Set<LaborRecord>()
                    .AddAsync(new LaborRecord(discrepId, employeeId, hours, date));
                await _applicationContext.SaveChangesAsync();
                _logger.LogInformation($"Successfuly saved new LaborRecord. DiscrepancyId: {discrepId}.");
            }
        }
    }
}