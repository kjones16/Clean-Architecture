using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cln.Application.Todo.Models
{
    public class TodoItemModel : TodoItemUpdateModel//, IValidatableObject
    {
        public long Id { get; set; }

        public long ListId { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{

            // Just an example. IValidatableObject can be used for additional validation.
            //return null;
            
            //if (string.IsNullOrWhiteSpace(Title) || Title.Length > 125)
            //{
            //    yield return new ValidationResult($"Title is required.", new[] { nameof(Title) });
            //}

            //if (Title?.Length > 125)
            //{
            //    yield return new ValidationResult($"Title can be no greater than 125 chars in length.", new[] { nameof(Title) });
            //}
        //}
    }
}
