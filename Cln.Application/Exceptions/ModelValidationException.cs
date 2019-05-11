using System;
using System.ComponentModel.DataAnnotations;

namespace Cln.Application.Exceptions
{
    public class ModelValidationException : Exception
    {
        public ValidationResult ValidationResult { get; set; }

        public ModelValidationException()
        {
        }

        public ModelValidationException(string message) : base(message)
        {
        }

        public ModelValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ModelValidationException(string message, string property) : base()
        {
            ValidationResult =  new ValidationResult(message, new[] { property });
        }
    }
}
