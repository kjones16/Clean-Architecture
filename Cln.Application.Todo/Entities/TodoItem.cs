using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cln.Application.Todo.Validators;

namespace Cln.Application.Todo.Entities
{
    public class TodoItem : ITodoEntity
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        [Required]
        [MaxLength(125)]
        [TodoUniqueTitleValidator]
        public string Title { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null; // Do class validation here.
        }
    }
}
