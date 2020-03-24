using System.Threading.Tasks;
using CAM.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace CAM.Core.Interfaces
{
    public interface IFileHandler
    {
        /// <summary>
        /// Attempts to save the given image and a thumbnail to the configurated image directory and update the Part's properties.
        /// Returns an unchanged part on any exception.
        /// </summary>
        Task<Part> SetPartImage(Part part, IFormFile image);
    }
}