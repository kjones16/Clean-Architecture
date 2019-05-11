using System.ComponentModel;

namespace Cln.Application.Todo.Models
{
    public class TodoItemUpdateModel : TodoItemCreateModel
    {
        [DefaultValue(false)]
        public bool IsComplete { get; set; }
    }
}
