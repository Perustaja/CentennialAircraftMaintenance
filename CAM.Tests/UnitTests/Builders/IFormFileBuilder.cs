using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CAM.Tests.UnitTests.Builders
{
    public class IFormFileBuilder
    {
        public static IFormFile CreateNullFormFile()
        {
            return new FormFile(new MemoryStream(Encoding.UTF8.GetBytes(String.Empty)), 0, 0, String.Empty, String.Empty);
        }
        /// <param name="fileName">The name of the mock file with extensions e.g. foo.txt</param>
        public static IFormFile CreateMockFormFile(string fileName, int length = 0)
        {
            return new FormFile(
                baseStream: new MemoryStream(Encoding.UTF8.GetBytes(String.Empty)),
                baseStreamOffset: 0,
                length: length,
                name: "test",
                fileName: fileName
            );
        }
    }
}