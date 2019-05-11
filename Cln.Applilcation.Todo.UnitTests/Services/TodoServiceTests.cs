using AutoMapper;
using Cln.Application.Todo.Models;
using Cln.Application.Todo.Services.TodoItem;
using Cln.Infrastructure.Todo.Database;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using System.Collections.Generic;
using System;
using Xunit;
using System.Threading.Tasks;
using Cln.Entities.Todo;
using Cln.Application.Exceptions;

namespace Cln.Applilcation.Todo.UnitTests.Services
{
    public class TodoServiceTests
    {
        [Fact]
        public void CreateTodoItem_ParentListIsMissing_ReturnKeyNotFound()
        {
            var context = CreateTodoDbContext();
            var todoService = CreateTodoItemService(context);

            Func<Task> action = async () => await todoService.CreateTodoItem(1, new TodoItemCreateModel());

            action.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void CreateTodoItem_TitleAlreadyExistsList_ReturnModelValidationException()
        {
            var context = CreateTodoDbContext();

            // Setup data for test.
            context.Add(new TodoList() { Id = 1, Title = "List1", ProjectId = 1 });
            context.Add(new TodoItem() { Id = 1, ListId = 1, Title = "TestTitle" });
            context.SaveChanges(true);

            var todoService = CreateTodoItemService(context);

            Func<Task> action = async () => await todoService.CreateTodoItem(1, new TodoItemCreateModel() { Title = "TestTitle" });

            action.Should().Throw<ModelValidationException>();
        }

        [Fact]
        public async void CreateTodoItem_InputIsValid_ReturnNewModel()
        {
            var context = CreateTodoDbContext();

            context.Add(new TodoList() { Id = 1, Title = "List1", ProjectId = 1 });
            context.SaveChanges(true);

            var todoService = CreateTodoItemService(context);

            var result = await todoService.CreateTodoItem(1, new TodoItemCreateModel() { Title = "New TodoItem" });

            result.Should().BeEquivalentTo(new TodoItemModel() {  Id = 1, ListId = 1, IsComplete = false, Title = "New TodoItem" });
        }

        private ITodoItemService CreateTodoItemService(TodoDbContext todoDbContext)
        {
            var mapper = new Mapper(new MapperConfiguration(m => m.AddProfile<TodoMappingProfile>()));

            return new TodoItemService(new TodoItemDbRepository(todoDbContext), new TodoListDbRepository(todoDbContext), new TodoPersistentChanges(todoDbContext), mapper);
        }

        private TodoDbContext CreateTodoDbContext()
        {
            // See testing with in-memory context. https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
            var context = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "TestDbContext").Options);

            context.Database.EnsureDeleted(); // In-Memory is stored statically so we need to clear it before each test.

            return context;
        }
    }
}
