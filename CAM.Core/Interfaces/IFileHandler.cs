using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CAM.Core.Interfaces
{
    public interface IFileHandler
    {
        /// <summary>
        /// Attempts to save the image to the specified directory. If a failure occurs it returns the default image path.
        /// </summary>
        Task<string> TrySaveImageAndReturnPathAsync(string fileName, IFormFile image, string directory);
    }
}