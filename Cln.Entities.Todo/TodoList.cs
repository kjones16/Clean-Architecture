using System.Collections.Generic;

namespace Cln.Entities.Todo
{
    public class TodoList : IEntity
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public List<TodoItem> Items { get; set; }

        public long ProjectId { get; set; }
    }
}
