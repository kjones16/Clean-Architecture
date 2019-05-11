using Cln.Entities.Todo;
using Cln.Application.Interfaces;
using System.Threading.Tasks;

namespace Cln.Application.Todo.Interfaces
{
    public interface ITodoItemRepository: IRepository<TodoItem, long>
    {
        Task<bool> ExistsByTitle(long listId, string title);
    }
}