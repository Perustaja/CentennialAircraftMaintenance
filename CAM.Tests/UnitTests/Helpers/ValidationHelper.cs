using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CAM.Tests.UnitTests.Builders;

namespace CAM.Tests.UnitTests.Helpers
{
    public class ValidationHelper
    {
        public static IList<ValidationResult> ValidateRequiredFile(ImageModel model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model);
            context.MemberName = "RequiredFile";
            Validator.TryValidateProperty(model.RequiredFile, context, validationResults);
            return validationResults;
        }
        public static IList<ValidationResult> ValidateNotRequiredFile(ImageModel model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model);
            context.MemberName = "NotRequiredFile";
            Validator.TryValidateProperty(model.NotRequiredFile, context, validationResults);
            return validationResults;
        }
    }
}