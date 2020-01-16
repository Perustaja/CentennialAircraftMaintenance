using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using CAM.Core.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace CAM.Core.Attributes
{
    public class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        protected readonly List<string> _extensions;
        public AllowedFileExtensionsAttribute(params string[] extensions)
        {
            _extensions = extensions.ToList();
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var file = value as IFormFile;
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            
            if (String.IsNullOrEmpty(fileExtension) || !_extensions.Contains(fileExtension))
            {
                return new ValidationResult("The file's extension is not valid.");
            }

            return ValidationResult.Success;
        }
    }
}