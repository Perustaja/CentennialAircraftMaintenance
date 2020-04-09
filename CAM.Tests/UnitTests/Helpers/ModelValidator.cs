using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAM.Tests.UnitTests.Helpers
{
    public class ModelValidator
    {
        public static IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }
    }
}