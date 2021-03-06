﻿using System.Threading.Tasks;
using AutoMapper;
using Cln.Application.Todo.Interfaces;
using Cln.Entities.Todo;
using Microsoft.EntityFrameworkCore;

namespace Cln.Infrastructure.Todo.Database
{
    public class TodoListDbRepository : DbContextRepository<TodoList, long>, ITodoListRepository
    {
        private readonly TodoDbContext _dbContext;

        public TodoListDbRepository(TodoDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByTitleAsync(long projectId, string title)
        {
            return await _dbContext.TodoLists.AnyAsync(tl => tl.ProjectId == projectId && tl.Title == title);
        }
    }
}
