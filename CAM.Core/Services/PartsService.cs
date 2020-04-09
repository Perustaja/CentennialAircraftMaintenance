using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Interfaces;
using CAM.Core.Interfaces.Repositories;
using CAM.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using CAM.Core.SharedKernel;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace CAM.Core.Services
{
    public class PartsService : IPartsService
    {
        private readonly ILogger<PartsService> _logger;
        private readonly IPartRepository _partRepository;
        private readonly IFileHandler _fileHandler;
        public PartsService(ILogger<PartsService> logger, IPartRepository repo, IFileHandler fileHandler)
        {
            _logger = logger;
            _partRepository = repo;
            _fileHandler = fileHandler;
        }
        public async Task<PaginatedList<Part>> GetPaginatedPartsBySearchParams(string search, string filter, int page = 1, int ipp = 10)
        {
            var queryable = _partRepository.GetBySearchParamsAsync(search, filter, page, ipp);
            return await PaginatedList<Part>.CreateAsync(queryable, page, ipp);
        }
        public async Task<Part> GetPartOrDefaultByMfrsPartNumber(string mfrsPartNumber, bool inclTracking)
        {
            return await _partRepository.GetByMfrsPnAsync(mfrsPartNumber, inclTracking);
        }
        public async Task<Part> GetPartOrDefaultById(int id, bool inclTracking)
        {
            return await _partRepository.GetByIdAsync(id, inclTracking);
        }
        public async Task<bool> PartExists(string mfrsPartNumber)
        {
            return await _partRepository.CheckForExistingRecordByPnAsync(mfrsPartNumber);
        }
        public async Task<bool> TryCreatePart(string mfrsPartNumber, int partCategoryId, string cataloguePartNumber, string name, string description,
        decimal priceIn, decimal? priceOut, string vendor, int? minimumStock, IFormFile image)
        {
            var part = new Part(mfrsPartNumber, partCategoryId, cataloguePartNumber, name, description,
            priceIn, priceOut, vendor, minimumStock);

            try
            {
                await _partRepository.AddAsync(part);
                part = await _fileHandler.SetPartImage(part, image);
                await _partRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // log exception
                return false;
            }

        }
        public async Task<bool> TryEditPart(int id, string mfrsPartNumber, int partCategoryId, string cataloguePartNumber, string name, string description,
        decimal priceIn, decimal? priceOut, string vendor, int? minimumStock, IFormFile image)
        {
            var part = await _partRepository.GetByIdAsync(id);
            part.EditPart(mfrsPartNumber, partCategoryId, cataloguePartNumber, name, description,
            priceIn, priceOut, vendor, minimumStock);
            part = await _fileHandler.SetPartImage(part, image);

            try
            {
                await _partRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // log excep
                return false;
            }
        }
        public async Task<bool> TryDeletePart(int id)
        {
            var part = await _partRepository.GetByIdAsync(id);
            try
            {
                await _partRepository.DeleteAsync(part);
                return true;
            }
            catch (Exception)
            {
                // log excep
                return false;
            }
        }
        public async Task<bool> TryReceiveShipment(List<int> ids, List<int> qtys)
        {
            if (qtys.Any(q => q < 1))
                return false;
            foreach (var i in ids)
            {
                if (!await _partRepository.CheckForExistingRecordByIdAsync(i))
                    return false;
            }
            var tasksAndQtys = new List<KeyValuePair<Task<Part>, int>>();
            for (int i = 0; i < ids.Count; i++)
                tasksAndQtys.Add(new KeyValuePair<Task<Part>, int>(_partRepository.GetByIdAsync(ids[i]), qtys[i]));
            
            var partsAndQtys = await Task
                .WhenAll(tasksAndQtys.Select(async kvp => new KeyValuePair<Part, int>(await kvp.Key, kvp.Value))
            );
            try
            {
                foreach (var kvp in partsAndQtys)
                    kvp.Key.AddStock(kvp.Value);
                await _partRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // log
                return false;
            }
        }
    }
}