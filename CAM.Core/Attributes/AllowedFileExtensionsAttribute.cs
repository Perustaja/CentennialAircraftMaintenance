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
            try
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!String.IsNullOrEmpty(fileExtension) && _extensions.Contains(fileExtension))
                    return ValidationResult.Success;
                else
                    return new ValidationResult("The given file's extension is not valid.");
            }
            catch (System.NullReferenceException) // file was not entered
            {
                if (_required)
                    return new ValidationResult("An image is required.");
                else
                    return ValidationResult.Success;
            }
        }
    }
}