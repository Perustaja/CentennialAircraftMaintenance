using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CAM.Core.Interfaces
{
    public interface IFileHandler
    {
        Task<string> TrySaveImageAndReturnPathAsync(string fileName, IFormFile image, string directory);
    }
}