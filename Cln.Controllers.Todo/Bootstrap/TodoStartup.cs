using Cln.Application.Todo.Interfaces;
using Cln.Application.Todo.Services.TodoItem;
using Cln.Application.Todo.Services.TodoList;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Cln.Infrastructure.Todo.Database;

namespace Cln.Controllers.Todo.Bootstrap
{
    public static class TodoStartup
    {
        public static void ConfigureServices(IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            services.AddTransient<ITodoItemService, TodoItemService>();
            services.AddTransient<ITodoListService, TodoListService>();

            services.AddScoped<ITodoItemRepository, TodoItemDbRepository>();
            services.AddScoped<ITodoListRepository, TodoListDbRepository>();
            services.AddScoped<ITodoPersistentChanges, TodoPersistentChanges>();

            services.AddDbContext<TodoDbContext>(options =>
            {
                options.UseSqlite(new SqliteConnection("DataSource=Project.db"), b => b.MigrationsAssembly("Cln.Web.Api")); // Move somewhere else. 
            });

            mvcBuilder.AddApplicationPart(Assembly.GetAssembly(typeof(TodoStartup))).AddControllersAsServices();
        }

        public static Assembly GetAssembly()
        {
            return Assembly.GetAssembly(typeof(TodoStartup));
        }
    }
}
