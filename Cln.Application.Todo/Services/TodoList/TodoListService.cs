using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cln.Application.Exceptions;
using Cln.Application.Todo.Interfaces;
using Cln.Application.Todo.Models;

namespace Cln.Application.Todo.Services.TodoList
{
    public class TodoListService : ITodoListService
    {
        private const string TitleExistMessage = "A todo list with that title already exist in the project.";

        private readonly ITodoListRepository _todoListRepository;
        private readonly ITodoPersistentChanges _todoUnitOfWork;
        private readonly IMapper _mapper;

        public TodoListService(ITodoListRepository todoListRepository, ITodoPersistentChanges todoUnitOfWork, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _todoUnitOfWork = todoUnitOfWork;
            _mapper = mapper;
        }

        public async Task<TodoListModel> AddTodoList(long projectId, TodoListUpsertModel todoList)
        {
            if (await _todoListRepository.ExistsByTitleAsync(projectId, todoList.Title))
                throw new ModelValidationException(TitleExistMessage, nameof(todoList.Title));

            var entity = new Entities.Todo.TodoList()
            {
                ProjectId = projectId,
                Title = todoList.Title
            };

            _todoListRepository.Insert(entity);

            await _todoUnitOfWork.SaveChanges();

            return _mapper.Map<TodoListModel>(entity);
        }

        public async Task DeleteTodoList(long todoListId)
        {
            var existingValue = await _todoListRepository.GetById(todoListId);

            if (existingValue == null)
                throw new KeyNotFoundException();

            _todoListRepository.Delete(existingValue);

            await _todoUnitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<TodoListModel>> GetAllTodoLists(long projectId)
        {
            return await _todoListRepository.Get<TodoListModel>(e => e.ProjectId == projectId);
        }

        public async Task<TodoListModel> GetTodoList(long todoListId)
        {
            var existingValue = await _todoListRepository.GetById(todoListId);

            if (existingValue == null)
                throw new KeyNotFoundException();

            return _mapper.Map<TodoListModel>(existingValue);
        }

        public async Task<TodoListModel> UpdateTodoList(long projectId, long listId, TodoListUpsertModel todoList)
        {
            var existingValue = await _todoListRepository.GetById(listId);

            if (existingValue == null)
                throw new KeyNotFoundException();

            if (await _todoListRepository.ExistsByTitleAsync(projectId, todoList.Title))
                throw new ModelValidationException(TitleExistMessage, nameof(todoList.Title));

            existingValue.Title = todoList.Title;

            await _todoUnitOfWork.SaveChanges();

            return _mapper.Map<TodoListModel>(existingValue);
        }
    }
}
