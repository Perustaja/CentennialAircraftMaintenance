using CAM.Core.Attributes;
using Microsoft.AspNetCore.Http;

namespace CAM.Tests.UnitTests.Builders
{
    public class ImageModel
    {
        [AllowedFileExtensions(true, ".jpg", ".png")]
        public IFormFile RequiredFile { get; set; }
        [AllowedFileExtensions(false, ".jpg", ".png")]
        public IFormFile NotRequiredFile { get; set; }
    }
}