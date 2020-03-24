using Xunit;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using System;
using CAM.Core.SharedKernel;
using CAM.Core.Interfaces;
using System.Threading.Tasks;
using CAM.Core.Entities;
using CAM.Infrastructure.Services;
using Microsoft.Extensions.Logging.Abstractions;
using CAM.Tests.Builders;

namespace CAM.Tests.UnitTests.Infrastructure.Services
{
    public class FileHandlerTests
    {
        private readonly IFileHandler _fileHandler = new FileHandler(new NullLogger<FileHandler>());
        private Part _mockPart = new Part("TEST", 0, null, null, null, 0m, null, null, null);

        [Fact]
        public async Task Returns_Unchanged_Part_If_IFormFile_Null()
        {
            var expectedImagePath = _mockPart.ImagePath;
            var expectedThumbPath = _mockPart.ImageThumbPath;
            var nullFile = IFormFileBuilder.CreateNullFormFile();

            var modifiedPart = await _fileHandler.SetPartImage(_mockPart, nullFile);

            Assert.Equal(expectedImagePath, modifiedPart.ImagePath);
            Assert.Equal(expectedThumbPath, modifiedPart.ImageThumbPath);
        }

        [Theory]
        [InlineData("test.txt")]
        [InlineData("spooky.exe")]
        [InlineData("invalid")]
        public async Task Returns_Unchanged_Part_If_IFormFile_Extension_Invalid(string fileName)
        {
            var expectedImagePath = _mockPart.ImagePath;
            var expectedThumbPath = _mockPart.ImageThumbPath;
            var invalidFile = IFormFileBuilder.CreateMockFormFile(fileName);

            var modifiedPart = await _fileHandler.SetPartImage(_mockPart, invalidFile);

            Assert.Equal(expectedImagePath, modifiedPart.ImagePath);
            Assert.Equal(expectedThumbPath, modifiedPart.ImageThumbPath);
        }
        
        // Further tests to be written upon configuration with Azure blob storage
    }
}