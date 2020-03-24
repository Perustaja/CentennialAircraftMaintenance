using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CAM.Tests.Builders
{
    public class IFormFileBuilder
    {
        public static IFormFile CreateNullFormFile()
        {
            return new FormFile(new MemoryStream(Encoding.UTF8.GetBytes(String.Empty)), 0, 0, String.Empty, String.Empty);
        }
        public static IFormFile CreateMockFormFile(string mockName)
        {
            return new FormFile(
                baseStream: new MemoryStream(Encoding.UTF8.GetBytes(String.Empty)),
                baseStreamOffset: 0,
                length: 0,
                name: "test",
                fileName: mockName
            );
        }
    }
}