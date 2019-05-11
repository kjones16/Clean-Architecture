using System.Collections.Generic;

namespace Cln.Application.Todo.Entities
{
    public class TodoList
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<TodoItem> Items { get; set; }

        public bool IsShared { get; set; }
    }
}
