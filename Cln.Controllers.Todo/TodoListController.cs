using System.Collections.Generic;
using System.Threading.Tasks;
using Cln.Application.Todo.Models;
using Cln.Application.Todo.Services.TodoList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cln.Controllers.Todo
{
    [Produces("application/json")]
    [Route("api")] // API Design https://docs.microsoft.com/en-us/azure/architecture/best-practices/api-design
    public class TodoListController : Controller
    {
        private readonly ITodoListService _todoService;

        public TodoListController(ITodoListService todoService) : base()
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Deletes a todo list and all of it's todo items.
        /// </summary>
        /// <param name="id">The id of the todo list.</param>
        [HttpDelete("todolist/{id}")]
        [ProducesResponseType(typeof(TodoListModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task Delete(long id)
        {
            await _todoService.DeleteTodoList(id);

            Accepted();
        }

        /// <summary>
        /// Gets a todo list entry.
        /// </summary>
        /// <param name="id">The id of the todo list.</param>
        [HttpGet("todolist/{id}", Name = "GetTodoList")]
        [ProducesResponseType(typeof(TodoListModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodoList(long id)
        {
            return Ok(await _todoService.GetTodoList(id));
        }

        /// <summary>
        /// Get all the todo lists in a project.
        /// </summary>
        /// <param name="projectId">The id of the project.</param>
        [HttpGet("project/{projectId:long}/todolist", Name = "GetTodoLists")]
        [ProducesResponseType(typeof(IEnumerable<TodoListModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodoLists(long projectId)
        {
            return Ok(await _todoService.GetAllTodoLists(projectId));
        }

        /// <summary>
        /// Creates a new todo list in a given project.
        /// </summary>
        /// <param name="projectId">The id of the project to add the list to.</param>
        /// <param name="value">The new todo list.</param>
        [HttpPost("project/{projectId:long}/todolist")]
        [ProducesResponseType(typeof(TodoListCreateModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(long projectId, [FromBody] TodoListCreateModel value)
        {
            var result = await _todoService.AddTodoList(projectId, value);

            return CreatedAtAction(nameof(GetTodoList), new { projectId, id = result.Id}, result);
        }

        /// <summary>
        /// Updates an existing todo list.
        /// </summary>
        /// <param name="listId">The id of the list to update.</param>
        /// <param name="value">The updated list values</param>
        [HttpPut("todolist/{listId:long}")]
        [ProducesResponseType(typeof(TodoListModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task Put(long listId, [FromBody] TodoListUpdateModel value)
        {
            await _todoService.UpdateTodoList(listId, value);

            Accepted();
        }
    }
}