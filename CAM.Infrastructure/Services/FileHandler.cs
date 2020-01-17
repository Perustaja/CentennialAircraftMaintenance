using System;
using System.IO;
using System.Threading.Tasks;
using CAM.Core.Interfaces;
using CAM.Core.SharedKernel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CAM.Infrastructure.Services
{
    /// <summary>
    /// Takes in a filename, a image, and the directory to save to. If the image is null, it simply returns
    /// the default value without any saving. If saving fails, the default value is also returned and an error is logged.
    /// </summary>
    public class FileHandler : IFileHandler
    {
        private readonly ILogger<FileHandler> _logger;
        public FileHandler(ILogger<FileHandler> logger)
        {
            _logger = logger;
        }
        public async Task<string> TrySaveImageAndReturnPathAsync(string fileName, IFormFile image, string directory)
        {
            string defaultPath = $"{directory}/{Constants.DEFAULT_IMAGE_NAME}";
            string filePath;
            if (image != null && Path.GetExtension(image.FileName) != null)
            {
                filePath = $"{directory}/{fileName.ToUpper()}{Path.GetExtension(image.FileName).ToLowerInvariant()}";
                try
                {
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        _logger.LogInformation($"{DateTime.Now}: Attempting to save image: {filePath}.");
                        await image.CopyToAsync(stream);
                        _logger.LogInformation($"{DateTime.Now}: Successfully saved image: {filePath}.");
                        return filePath;
                    }
                }
                catch (Exception exc)
                {
                    _logger.LogError($"{DateTime.Now}: Unable to save image: {filePath}. {exc.Message}");
                    return defaultPath;
                }
            }
            return defaultPath;
        }
    }
}