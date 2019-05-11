using Cln.Application.Todo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cln.Application.Todo.Services.TodoItem
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemModel>> GetAllTodoItems(long listId);

        Task<TodoItemModel> CreateTodoItem(long listId, TodoItemCreateModel todoItem);

        Task<TodoItemModel> GetTodoItem(long todoItemId);

        Task DeleteTodoItem(long todoItemId);

        Task<TodoItemModel> UpdateTodoItem(long todoItemId, TodoItemUpdateModel todoItem);

        Task MarkAllComplete(long listId);
    }
}