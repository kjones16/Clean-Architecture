using System.Threading.Tasks;
using Cln.Application.Todo.Interfaces;

namespace Cln.Infrastructure.Todo.Database
{
    public class TodoPersistentChanges : ITodoPersistentChanges
    {
        private TodoDbContext _dbContext;

        public TodoPersistentChanges(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
