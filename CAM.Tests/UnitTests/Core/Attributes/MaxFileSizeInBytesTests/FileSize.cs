using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CAM.Core.Attributes;
using CAM.Tests.UnitTests.Builders;
using CAM.Tests.UnitTests.Helpers;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace CAM.Tests.UnitTests.Core.Attributes.MaxFileSizeInBytesTests
{
    public class FileSize
    {
        [Fact]
        public void Returns_ValidationResult_If_Size_Exceeds_Spec()
        {
            var oneByteOver5Mb = 5242881;
            var largeFile = IFormFileBuilder.CreateMockFormFile("Foo", oneByteOver5Mb);

            var invalidModel = new Model()
            {
                File = largeFile
            };
            
            Assert.True(ModelValidator.ValidateModel(invalidModel).Count > 0);
        }
    }
    public class Model
    {
        [MaxFileSizeInBytes(5 * 1024 * 1024)] //5 MB
        public IFormFile File { get; set; }
    }
}