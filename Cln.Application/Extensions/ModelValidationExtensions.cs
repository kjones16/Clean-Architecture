using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cln.Application.Models;
using System.Linq;

namespace Cln.Application.Common
{
    public static class ModelValidationExtensions
    {
        public static List<ValidationResult> Validate(this Model model)
        {
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            Validator.TryValidateObject(model, context, result, true);

            return result;
        }

        public static bool IsValid(this Model model)
        {
            return !model.Validate().Any();
        }

        public static void AddError(this List<ValidationResult> results, string message, string propertyName)
        {
            results.Add(new ValidationResult("A todo with that title already exist in the list.", new[] { propertyName }));
        }
    }
}
