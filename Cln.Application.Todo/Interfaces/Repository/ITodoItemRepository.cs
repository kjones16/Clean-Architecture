using Cln.Application.Domain.Todo;

namespace Cln.Application.Todo.Interfaces.Repository
{
    public interface ITodoItemRepository: IRepository<TodoItem, int>
    {
        bool ExistsByTitle(int projectId, string title);
    }
}