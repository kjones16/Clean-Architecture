using System.ComponentModel.DataAnnotations;

namespace Cln.Application.Todo.Models
{
    public class TodoItemModel : TodoItemUpdateModel
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long ListId { get; set; }
    }
}
