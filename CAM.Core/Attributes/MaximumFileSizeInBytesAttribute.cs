using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using CAM.Core.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace CAM.Core.Attributes
{
    public class MaxFileSizeInBytesAttribute : ValidationAttribute
    {
        protected readonly int _size;
        public MaxFileSizeInBytesAttribute(int size)
        {
            _size = size;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var file = value as IFormFile;
            if (file.Length > _size)
            {
                return new ValidationResult($"The file's size cannot exceed {_size} bytes");
            }
            return ValidationResult.Success;
        }
    }
}