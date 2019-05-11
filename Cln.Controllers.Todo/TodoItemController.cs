using System.Collections.Generic;
using System.Threading.Tasks;
using Cln.Application.Todo.Models;
using Cln.Application.Todo.Services.TodoItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cln.Controllers.Todo
{
    [Produces("application/json")]
    [Route("api/project/{projectId:long}")] // API Design https://docs.microsoft.com/en-us/azure/architecture/best-practices/api-design, https://mathieu.fenniak.net/the-api-checklist/
    public class TodoItemController : Controller
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemController(ITodoItemService todoService) : base()
        {
            _todoItemService = todoService;
        }

        [HttpGet("todoitem/{id:long}", Name = "GetTodoItem")]
        [ProducesResponseType(typeof(TodoItemModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodoItem(long projectId, long id)
        {
            return Ok(await _todoItemService.GetTodoItem(id));
        }

        [HttpGet("todolist/{listId:long}/todoitem", Name = "GetTodoItems")]
        [ProducesResponseType(typeof(IEnumerable<TodoItemModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodoItems(long projectId, long listId)
        {
            return Ok(await _todoItemService.GetAllTodoItems(listId));
        }

        [HttpPost("todolist/{listId:long}/todoitem")]
        [ProducesResponseType(typeof(TodoItemUpdateModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(long projectId, long listId, [FromBody] TodoItemCreateModel value)
        {
            var result = await _todoItemService.CreateTodoItem(listId, value);

            if (result != null)
                return CreatedAtAction(nameof(GetTodoItem), new { projectId, id = result.Id }, result);

            return BadRequest();
        }

        [HttpPut("todoitem/{id:long}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task Update(long projectId, long id, [FromBody] TodoItemUpdateModel value)
        {
            await _todoItemService.UpdateTodoItem(id, value);

            Accepted();
        }

        [HttpPut("todolist/{listId:long}/todoitem/markallcomplete")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task MarkAllComplete(long projectId, long listId)
        {
            await _todoItemService.MarkAllComplete(listId);

            Accepted();
        }

        [HttpDelete("todoitem/{id:long}")]
        [ProducesResponseType(typeof(TodoItemModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task Delete(long projectId, long id)
        {
            await _todoItemService.DeleteTodoItem(id);

            Accepted();
        }
    }
}