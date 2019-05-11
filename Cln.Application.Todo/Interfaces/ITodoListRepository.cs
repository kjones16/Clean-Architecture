using Cln.Application.Interfaces;
using Cln.Entities.Todo;
using System.Threading.Tasks;

namespace Cln.Application.Todo.Interfaces
{
    public interface ITodoListRepository : IRepository<TodoList, long>
    {
        Task<bool> ExistsByTitleAsync(long projectId, string title);
    }
}
