using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CAM.Core.Attributes
{
    public class MaxFileSizeInBytesAttribute : ValidationAttribute
    {
        protected readonly int _size;
        /// <summary>
        /// Validates based on a specified max value size in binary bytes.
        /// </summary>
        public MaxFileSizeInBytesAttribute(int size)
        {
            _size = size;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var file = value as IFormFile;
            if (file != null && file.Length > _size)
            {
                return new ValidationResult($"The file's size cannot exceed {_size} bytes");
            }
            return ValidationResult.Success;
        }
    }
}