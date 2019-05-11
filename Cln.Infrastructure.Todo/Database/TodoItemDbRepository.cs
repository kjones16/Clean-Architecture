using System.Threading.Tasks;
using Cln.Application.Todo.Interfaces;
using Cln.Entities.Todo;
using Microsoft.EntityFrameworkCore;

namespace Cln.Infrastructure.Todo.Database
{
    public class TodoItemDbRepository : DbContextRepository<TodoItem, long>, ITodoItemRepository
    {
        private readonly TodoDbContext _dbContext;

        public TodoItemDbRepository(TodoDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByTitle(long listId, string title)
        {
            return await _dbContext.TodoItems.AnyAsync(ti => ti.ListId == listId && ti.Title == title);
        }
    }
}