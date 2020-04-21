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
        public DiscrepancyService(ILogger<DiscrepancyService> logger, IDiscrepancyRepository discrepRepo,
        IPartRepository partRepo)
        {
            _logger = logger;
            _discrepRepo = discrepRepo;
            _partRepo = partRepo;
        }
        public async Task<Discrepancy> GetDiscrepOrDefaultByIndex(int wOrderId, int index, bool inclTracking)
        {
            return await _discrepRepo.GetByWorkOrderAndIndexOrDefault(wOrderId, index, inclTracking);
        }
        public async Task<List<DiscrepancyPart>> GetDiscrepancyPartsById(int discrepId)
        {
            return await _discrepRepo.GetDiscrepancyPartsById(discrepId);
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
    }
}