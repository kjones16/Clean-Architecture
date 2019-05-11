namespace Cln.Entities.Todo
{
    public class TodoItem : IEntity
    {
        public long Id { get; set; }

        public long ListId { get; set; }

        public TodoList List { get; set; }

        public string Title { get; set; }

        public bool IsComplete { get; set; }
    }
}
