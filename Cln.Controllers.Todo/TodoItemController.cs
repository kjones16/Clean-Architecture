using System.Collections.Generic;
using System.Threading.Tasks;
using Cln.Application.Todo.Models;
using Cln.Application.Todo.Services.TodoItem;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cln.Controllers.Todo
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api")]
    public class TodoItemController : Controller
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemController(ITodoItemService todoService) : base()
        {
            _todoItemService = todoService;
        }

        /// <summary>
        /// Get a single todo item.
        /// </summary>
        /// <param name="id">The id of the todo item</param>
        [HttpGet("todoitem/{id:long}", Name = "GetTodoItem")]
        [ProducesResponseType(typeof(TodoItemModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodoItem(long id)
        {
            return Ok(await _todoItemService.GetTodoItem(id));
        }

        /// <summary>
        /// Gets all the todo items in a list.
        /// </summary>
        /// <param name="listId">The id of the list to get todo items for</param>
        /// <returns></returns>
        [HttpGet("todolist/{listId:long}/todoitem", Name = "GetTodoItems")]
        [ProducesResponseType(typeof(IEnumerable<TodoItemModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodoItems(long listId)
        {
            return Ok(await _todoItemService.GetAllTodoItems(listId));
        }

        /// <summary>
        /// Creates and adds a todo item to a todo list.
        /// </summary>
        /// <param name="listId">The id to the todo list to add to.</param>
        /// <param name="value">The values for the new todo item</param>
        [HttpPost("todolist/{listId:long}/todoitem")]
        [ProducesResponseType(typeof(TodoItemUpdateModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTodoItem(long listId, [FromBody] TodoItemCreateModel value)
        {
            var result = await _todoItemService.CreateTodoItem(listId, value);

            if (result != null)
                return CreatedAtAction(nameof(GetTodoItem), new { id = result.Id }, result);

            return BadRequest();
        }

        /// <summary>
        /// Updates an existing todo item.
        /// </summary>
        /// <param name="id">The id of the todo item to update.</param>
        /// <param name="value">The updated values to apply to the todo item.</param>
        [HttpPut("todoitem/{id:long}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task UpdateTodoItem(long id, [FromBody] TodoItemUpdateModel value)
        {
            await _todoItemService.UpdateTodoItem(id, value);

            Accepted();
        }

        /// <summary>
        /// Marks all the todo items in a todo list to complete.
        /// </summary>
        /// <param name="listId">The id of the todo list.</param>
        [HttpPut("todolist/{listId:long}/todoitem/markallcomplete")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task MarkAllComplete(long listId)
        {
            await _todoItemService.MarkAllComplete(listId);

            Accepted();
        }

        /// <summary>
        /// Deletes a todo item.
        /// </summary>
        /// <param name="id">The id of the todo item to delete.</param>
        [HttpDelete("todoitem/{id:long}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task DeleteTodoItem(long id)
        {
            await _todoItemService.DeleteTodoItem(id);

            Accepted();
        }
    }
}