using System.Collections.Generic;
using System.Threading.Tasks;
using Cln.Application.Todo.Models;

namespace Cln.Application.Todo.Services.TodoList
{
    public interface ITodoListService
    {
        Task<TodoListModel> AddTodoList(long projectId, TodoListUpsertModel todoList);

        Task DeleteTodoList(long todoListId);

        Task<IEnumerable<TodoListModel>> GetAllTodoLists(long projectId);

        Task<TodoListModel> GetTodoList(long todoListId);

        Task<TodoListModel> UpdateTodoList(long projectId, long listId, TodoListUpsertModel todoList);
    }
}