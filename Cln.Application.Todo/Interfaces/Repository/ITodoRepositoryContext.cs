namespace Cln.Application.Todo.Interfaces.Repository
{
    public interface ITodoRepositoryContext
    {
        ITodoItemRepository TodoItemRepository { get; }
    }
}