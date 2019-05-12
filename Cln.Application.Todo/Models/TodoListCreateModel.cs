using Cln.Application.Models;
using System.ComponentModel.DataAnnotations;

namespace Cln.Application.Todo.Models
{
    public class TodoListCreateModel : Model
    {
        [Required]
        [MaxLength(125)]
        public string Title { get; set; }
     }
}
