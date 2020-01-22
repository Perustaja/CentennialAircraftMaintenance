using Xunit;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using System;
using CAM.Core.SharedKernel;
using CAM.Core.Interfaces;
using System.Threading.Tasks;

namespace CAM.Tests.UnitTests.Infrastructure.Services
{
    public class FileHandlerTests
    {
        private readonly IFileHandler _fileHandler;
        public FileHandlerTests(IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }
        private IFormFile _nullFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes(String.Empty)), 0, 0, String.Empty, String.Empty);
        [Fact]
        public async Task Returns_Default_If_IFormFile_Null()
        {
            var dir = "foo";
            var expectedFilePath = $"foo/{Constants.DEFAULT_IMAGE_NAME}";

            var actualFilePath = await _fileHandler.TrySaveImageAndReturnPathAsync(String.Empty, _nullFile, dir);

            Assert.Equal(expectedFilePath, actualFilePath);
        }
    }
}