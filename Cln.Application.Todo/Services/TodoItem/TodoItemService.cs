using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cln.Application.Exceptions;
using Cln.Application.Todo.Interfaces;
using Cln.Application.Todo.Models;

namespace Cln.Application.Todo.Services.TodoItem
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly ITodoListRepository _todoListRepository;
        private readonly ITodoPersistentChanges _todoUnitOfWork;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemRepository todoItemRepository, ITodoListRepository todoListRepository, ITodoPersistentChanges todoUnitOfWork, IMapper mapper)
        {
            _todoItemRepository = todoItemRepository;
            _todoListRepository = todoListRepository;
            _todoUnitOfWork = todoUnitOfWork;
            _mapper = mapper;
        }

        public async Task<TodoItemModel> CreateTodoItem(long listId, TodoItemCreateModel todoItem)
        {
            if (await _todoListRepository.GetById(listId) == null)
                throw new KeyNotFoundException();

            if (await _todoItemRepository.ExistsByTitle(listId, todoItem.Title))
                throw new ModelValidationException("A todo with that title already exist in the list.", nameof(todoItem.Title));

            var entity = new Entities.Todo.TodoItem() { ListId = listId, Title = todoItem.Title, IsComplete = false  };

            entity.ListId = listId;

            _todoItemRepository.Insert(entity);

            await _todoUnitOfWork.SaveChanges();

            return _mapper.Map<TodoItemModel>(entity);
        }

        public async Task DeleteTodoItem(long todoItemId)
        {
            var existingValue = await _todoItemRepository.GetById(todoItemId);

            if (existingValue == null)
                return;

            _todoItemRepository.Delete(existingValue);

            await _todoUnitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<TodoItemModel>> GetAllTodoItems(long listId)
        {
            return await _todoItemRepository.Get<TodoItemModel>(e => e.ListId == listId);
        }

        public async Task<TodoItemModel> GetTodoItem(long todoItemId)
        {
            var foundItem = await _todoItemRepository.GetById(todoItemId);

            return _mapper.Map<TodoItemModel>(foundItem);
        }

        public async Task<TodoItemModel> UpdateTodoItem(long todoItemId, TodoItemUpdateModel todoItem)
        {
            var existingValue = await _todoItemRepository.GetById(todoItemId);

            if (existingValue == null)
                throw new KeyNotFoundException();

            existingValue.Title = todoItem.Title;
            existingValue.IsComplete = todoItem.IsComplete;

            await _todoUnitOfWork.SaveChanges();

            return _mapper.Map<TodoItemModel>(existingValue);
        }

        public async Task MarkAllComplete(long listId)
        {
            var todoItems = await _todoItemRepository.Get(e => e.ListId == listId);

            foreach(var todoItem in todoItems)
            {
                todoItem.IsComplete = true;
            }

            await _todoUnitOfWork.SaveChanges();
        }
    }
}