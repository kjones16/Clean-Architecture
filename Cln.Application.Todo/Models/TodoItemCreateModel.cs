using System.ComponentModel.DataAnnotations;
using Cln.Application.Models;

namespace Cln.Application.Todo.Models
{
    public class TodoItemCreateModel : Model
    {
        /// <summary>
        /// The title must be unique within the list it belongs to.
        /// </summary>
        [Required]
        [MaxLength(125)]
        public string Title { get; set; }
    }
}
