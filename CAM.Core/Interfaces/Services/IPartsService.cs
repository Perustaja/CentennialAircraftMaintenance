using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace CAM.Core.Interfaces.Services
{
    /// <summary>
    /// Service for Inventory and Parts. For simplicity, it is the controller's responsibility
    /// to check if a part exists using the PartExists method or by checking if the part returned
    /// is null with the applicable PartOrDefault methods.
    /// </summary>
    public interface IPartsService
    {
        Task<PaginatedList<Part>> GetPaginatedPartsBySearchParams(string search, string filter, int page = 1, int ipp = 10);
        Task<Part> GetPartOrDefaultByMfrsPartNumber(string mfrsPartNumber, bool inclTracking);
        Task<Part> GetPartOrDefaultById(int id, bool inclTracking);
        Task<bool> PartExists(string mfrsPartNumber);
        Task<bool> TryCreatePart(string mfrsPartNumber, int partCategoryId, string cataloguePartNumber, string name, string description,
        decimal priceIn, decimal? priceOut, string vendor, int? minimumStock, IFormFile image);
        Task<bool> TryEditPart(int id, string mfrsPartNumber, int partCategoryId, string cataloguePartNumber, string name, string description,
        decimal priceIn, decimal? priceOut, string vendor, int? minimumStock, IFormFile image);
        Task<bool> TryDeletePart(int id);
        /// <param name="qtysAndIds">A Dictionary with key quanity and value id</param>
        Task<bool> TryReceiveShipment(List<int> Ids, List<int> Qtys);
    }
}