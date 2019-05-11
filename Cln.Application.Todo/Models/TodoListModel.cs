using Cln.Application.Models;

namespace Cln.Application.Todo.Models
{
    public class TodoListModel : Model
    {
        public long Id { get; set; }

        public string Title { get; set; }
    }
}
