using Cln.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Cln.Web
{
    public static class ModelStateExtensions
    {
        public static void AddModelError(this ModelStateDictionary modelState, ModelValidationException modelValidationException)
        {
            if (modelValidationException == null)
            {
                throw new ArgumentNullException("modelValidationException cannot be null.");
            }

            modelState.AddModelError(modelValidationException.ValidationResult);
        }

        public static void AddModelError(this ModelStateDictionary modelState, ValidationResult validationResult)
        {
            if (modelState == null)
            {
                throw new ArgumentNullException("modelState cannot be null");
            }

            if (validationResult.MemberNames == null || !validationResult.MemberNames.Any())
            {
                throw new ArgumentNullException("modelState.MemberNames must be set.");
            }

            if (validationResult == null)
            {
                throw new ArgumentNullException("validationResult cannot be null");
            }

            if (string.IsNullOrWhiteSpace(validationResult.ErrorMessage))
            {
                throw new ArgumentNullException("validationResult.ErrorMessage must be set.");
            }

            modelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
        }
    }
}
