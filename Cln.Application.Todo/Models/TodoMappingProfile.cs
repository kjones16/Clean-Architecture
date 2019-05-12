using AutoMapper;
using Cln.Entities.Todo;
using System.Collections.Generic;

namespace Cln.Application.Todo.Models
{
    public class TodoMappingProfile : Profile
    {
        public TodoMappingProfile()
        {
            CreateMap<TodoItem, TodoItem>(); // Setup for ProtectTo. If need it is used for returning entities from repository in an untracked state.

            CreateMap<TodoList, TodoList>();

            CreateMap<TodoItem, TodoItemModel>() // Setup for ProtectTo
                .ReverseMap();

            CreateMap<TodoList, TodoListModel>()  // Setup for ProtectTo
                .ReverseMap();
        }
    }
}
