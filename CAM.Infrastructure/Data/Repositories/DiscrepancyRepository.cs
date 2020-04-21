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
        public async Task<List<DiscrepancyPart>> GetDiscrepancyPartsById(int discrepId)
        {
            return await _applicationContext.Set<DiscrepancyPart>()
                .Where(e => e.DiscrepancyId == discrepId)
                .Include(e => e.Part)
                .ToListAsync();
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
    }
}