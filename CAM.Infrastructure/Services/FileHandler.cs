using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Core.Interfaces;
using CAM.Core.SharedKernel;
using Microsoft.AspNetCore.Hosting;
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
        /// <summary>
        /// Takes a Part and an image and attempts to create this image and update the Part with its path.
        /// On any error, returns the part with no modifications made. This assumes that extensions are filtered out prior by model validation.
        /// In the event of a duplicate, the previous image is overwritten.
        /// </summary>
        public async Task<Part> SetPartImage(Part part, IFormFile image)
        {
            string imageExt = String.Empty;
            if (image != null)
                imageExt = Path.GetExtension(image.FileName);
            if (imageExt == String.Empty || !Constants.ALLOWED_IMAGE_EXTENSIONS.Contains(imageExt))
            {
                _logger.LogError($"Unable to save new image and assign to part. The given IFormFile is null or has an invalid extension.");
                return part;
            }
            string imageSavePath = $"{Constants.PARTS_IMAGES_DIRECTORY}/{part.Id.ToUpper()}{Path.GetExtension(image.FileName).ToLowerInvariant()}";
            string imageThumbSavePath = $"{Constants.PARTS_THUMB_DIRECTORY}/{part.Id.ToUpper()}{Path.GetExtension(image.FileName).ToLowerInvariant()}";
            if (!await TrySaveImageAndThumb(image, imageSavePath, imageThumbSavePath))
            {
                File.Delete(imageSavePath);
                File.Delete(imageThumbSavePath);
                return part; // Error will be logged and part will remain unchanged
            }
            var imagePath = $"{Constants.PARTS_IMAGES_CONTENT_PATH}/{part.Id.ToUpper()}{Path.GetExtension(image.FileName).ToLowerInvariant()}";
            var imageThumbPath = $"{Constants.PARTS_THUMB_CONTENT_PATH}/{part.Id.ToUpper()}{Path.GetExtension(image.FileName).ToLowerInvariant()}";
            part.ChangeImage(imagePath, imageThumbPath);
            return part;
        }
        private async Task<bool> TrySaveImageAndThumb(IFormFile image, string imagePath, string imageThumbPath)
        {
            using (var stream = System.IO.File.Create(imagePath))
            {
                try
                {
                    _logger.LogInformation($"{DateTime.Now}: Attempting to save image and thumb at location: {imagePath}, {imageThumbPath}.");
                    await image.CopyToAsync(stream);
                    _logger.LogInformation($"{DateTime.Now}: Successfully saved image at location: {imagePath}.");
                    CreateThumbnail(stream).Save(imageThumbPath);
                    _logger.LogInformation($"{DateTime.Now}: Successfully saved thumb at location: {imageThumbPath}.");
                    return true;
                }
                catch (Exception exc)
                {
                    _logger.LogError($"{DateTime.Now}: Unable to save image or thumbnail: {exc.Message}");
                    return false;
                }
            }
        }
        private Image CreateThumbnail(Stream stream, int width = 75, int height = 75)
        {
            return Image.FromStream(stream).GetThumbnailImage(width, height, () => false, IntPtr.Zero);
        }
    }
}