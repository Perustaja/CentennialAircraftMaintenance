using Xunit;
using CAM.Tests.UnitTests.Builders;
using CAM.Tests.UnitTests.Helpers;
using CAM.Core.Attributes;
using Microsoft.AspNetCore.Http;

namespace CAM.Tests.UnitTests.Core.Attributes.AllowedFileExtensionsTests
{
    public class Required
    {
        [Fact]
        public void Returns_ValidationSuccess_If_Not_Required_And_File_Empty()
        {
            var model = new Model()
            {
                NotRequiredFile = null
            };

            Assert.True(ModelValidator.ValidateModel(model).Count == 0);
        }

        [Fact]
        public void Returns_ValidationResult_If_Not_Required_But_Still_Invalid()
        {
            var invalidFile = IFormFileBuilder.CreateMockFormFile("hi.exe");
            var model = new Model()
            {
                NotRequiredFile = invalidFile
            };

            Assert.True(ModelValidator.ValidateModel(model).Count > 0);
        }
        public class Model
        {
            [AllowedFileExtensions(false, ".jpg", ".png")]
            public IFormFile NotRequiredFile { get; set; }
        }
    }
}