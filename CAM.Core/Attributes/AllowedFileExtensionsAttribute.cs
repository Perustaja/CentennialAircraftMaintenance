using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace CAM.Core.Attributes
{
    public class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        protected readonly List<string> _extensions;
        protected readonly bool _required;
        /// <summary>
        /// IMPORTANT: This attribute only verifies that the file's extension is valid and the filename itself
        /// contains exactly one decimal and isn't empty. This is NOT a general file checking attribute that will check for illegal characters, etc.
        /// and should only be used in conjunction with some service to rename the file before saving.
        /// </summary>
        public AllowedFileExtensionsAttribute(bool required, params string[] extensions)
        {
            _extensions = extensions.ToList();
            _required = required;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var file = value as IFormFile;
            var singleDecimalRegex = @"^[^.]+\.+[^.]+\w+$";
            try
            {
                if (Regex.IsMatch(file.FileName, singleDecimalRegex))
                {
                    var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    if (_extensions.Contains(fileExtension))
                        return ValidationResult.Success;
                }
                return new ValidationResult("The given file's name or extension is invalid.");
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