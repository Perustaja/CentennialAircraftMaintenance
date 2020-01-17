using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CAM.Core.Attributes
{
    public class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        protected readonly List<string> _extensions;
        protected readonly bool _required;
        public AllowedFileExtensionsAttribute(bool required, params string[] extensions)
        {
            _extensions = extensions.ToList();
            _required = required;
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var file = value as IFormFile;
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant() ?? null;
            
            if (String.IsNullOrEmpty(fileExtension) || !_extensions.Contains(fileExtension))
            {
                return new ValidationResult("The given file's extensions are not valid.");
            }
            return ValidationResult.Success;
        }
    }
}