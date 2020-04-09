using Xunit;
using CAM.Tests.UnitTests.Builders;
using CAM.Tests.UnitTests.Helpers;

namespace CAM.Tests.UnitTests.Core.Attributes.AllowsFileExtensionsTests
{
    public class Required
    {
        [Fact]
        public void Returns_ValidationSuccess_If_Not_Required_And_File_Empty()
        {
            var model = new ImageModel()
            {
                NotRequiredFile = null
            };

            Assert.True(ValidationHelper.ValidateNotRequiredFile(model).Count == 0);
        }

        [Fact]
        public void Returns_ValidationResult_If_Not_Required_But_Still_Invalid()
        {
            var invalidFile = IFormFileBuilder.CreateMockFormFile("hi.exe");
            var model = new ImageModel()
            {
                NotRequiredFile = invalidFile
            };

            Assert.True(ValidationHelper.ValidateNotRequiredFile(model).Count > 0);
        }
    }
}