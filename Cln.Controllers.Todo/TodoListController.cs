using System.Collections.Generic;
using System.Threading.Tasks;
using Cln.Application.Todo.Models;
using Cln.Application.Todo.Services.TodoList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cln.Controllers.Todo
{
    [Produces("application/json")]
    [Route("api/project/{projectId:long}/todolist")] // API Design https://docs.microsoft.com/en-us/azure/architecture/best-practices/api-design
    public class TodoListController : Controller
    {
        private readonly ITodoListService _todoService;

        public TodoListController(ITodoListService todoService) : base()
        {
            _todoService = todoService;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TodoListModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task Delete(long projectId, long id)
        {
            await _todoService.DeleteTodoList(id);

            Accepted();
        }

        [HttpGet("{id}", Name = "GetTodoList")]
        [ProducesResponseType(typeof(TodoListModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodoList(long projectId, long id)
        {
            return Ok(await _todoService.GetTodoList(id));
        }

        [HttpGet(Name = "GetTodoLists")]
        [ProducesResponseType(typeof(IEnumerable<TodoListModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodoLists(long projectId)
        {
            return Ok(await _todoService.GetAllTodoLists(projectId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoListUpsertModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(long projectId, [FromBody] TodoListUpsertModel value)
        {
            var result = await _todoService.AddTodoList(projectId, value);

            return CreatedAtAction(nameof(GetTodoList), new { projectId, id = result.Id}, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(TodoListModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task Put(long projectId, long listId, [FromBody] TodoListUpsertModel value)
        {
            await _todoService.UpdateTodoList(projectId, listId, value);

            Accepted();
        }
    }
}