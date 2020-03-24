using CAM.Core.Attributes;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Xunit;
using System.IO;
using System.Text;
using System.IO.MemoryMappedFiles;

namespace CAM.Tests.UnitTests.Core.Attributes
{
    public class AllowedFileExtensions
    {
        [AllowedFileExtensions(false, ".jpg", ".png", "jpeg")]
        private IFormFile _testProperty { get; set; }
        private IFormFile _validFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
        private IFormFile _invalidFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");

        /// <summary>
        /// Ensures that 
        /// </summary>
        [Fact]
        public void Returns_ValidationSuccess_If_Extensions_Match()
        {
            //When

            //Then
        }

        /// <summary>
        /// Ensures that 
        /// </summary>
        [Fact]
        public void Returns_ValidationResult_If_Extensions_Dont_Match()
        {
            //Given

            //When

            //Then
        }
    }
}