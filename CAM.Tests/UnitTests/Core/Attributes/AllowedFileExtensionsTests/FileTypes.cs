using Xunit;
using CAM.Tests.UnitTests.Builders;
using CAM.Tests.UnitTests.Helpers;
using CAM.Core.Attributes;
using Microsoft.AspNetCore.Http;

namespace CAM.Tests.UnitTests.Core.Attributes.AllowedFileExtensionsTests
{
    /// <summary>
    /// It is very important to remember that these tests are written expecting the file's original name
    /// to be discarded. The only thing that matters is the extension, this is NOT a general file checking test,
    /// it is only designed to make sure that a valid extension is given and the filename isn't empty.
    /// </summary>
    public class FileTypes
    {
        [Fact]
        public void Returns_ValidationSuccess_If_Extensions_Match()
        {
            var validFile = IFormFileBuilder.CreateMockFormFile("valid.jpg");
            var model = new Model()
            {
                RequiredFile = validFile
            };

            Assert.True(ModelValidator.ValidateModel(model).Count == 0);
        }

        [Fact]
        public void Returns_ValidationResult_If_No_Extension()
        {
            var noExtFile = IFormFileBuilder.CreateMockFormFile("sneaky");
            var model = new Model()
            {
                RequiredFile = noExtFile
            };

            Assert.True(ModelValidator.ValidateModel(model).Count > 0);
        }
        [Theory]
        [InlineData("spooky.exe")]
        [InlineData("spooky.exe.jpg")]
        [InlineData("oops.")]
        public void Returns_ValidationResult_If_Invalid_Extension(string fileName)
        {
            var invalidFile = IFormFileBuilder.CreateMockFormFile(fileName);
            var model = new Model()
            {
                RequiredFile = invalidFile
            };

            Assert.True(ModelValidator.ValidateModel(model).Count > 0);
        }
        [Theory]
        [InlineData(".jpg")]
        [InlineData(".exe.jpg")]
        public void Returns_ValidationResult_If_Leading_Decimal(string fileName)
        {
            var justExtensions = IFormFileBuilder.CreateMockFormFile(fileName);
            var model = new Model()
            {
                RequiredFile = justExtensions
            };

            Assert.True(ModelValidator.ValidateModel(model).Count > 0);
        }
        public class Model
        {
            [AllowedFileExtensions(true, ".jpg", ".png")]
            public IFormFile RequiredFile { get; set; }
        }
    }
}