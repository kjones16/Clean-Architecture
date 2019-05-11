using System.ComponentModel.DataAnnotations;
using Cln.Application.Models;

namespace Cln.Application.Todo.Models
{
    public class TodoItemCreateModel : Model
    {
        [Required]
        [MaxLength(125)]
        public string Title { get; set; }
    }
}
