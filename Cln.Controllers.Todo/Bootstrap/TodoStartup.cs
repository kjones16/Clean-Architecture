using Cln.Application.Todo.Interfaces;
using Cln.Application.Todo.Services.TodoItem;
using Cln.Application.Todo.Services.TodoList;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Cln.Infrastructure.Todo.Database;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Cln.Controllers.Todo.Bootstrap
{
    public static class TodoStartup
    {
        public static void ConfigureServices(IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            services.AddTransient<ITodoItemService, TodoItemService>();
            services.AddTransient<ITodoListService, TodoListService>();

            // EF based classes should be scoped. https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.0
            services.AddScoped<ITodoItemRepository, TodoItemDbRepository>();
            services.AddScoped<ITodoListRepository, TodoListDbRepository>();
            services.AddScoped<ITodoPersistentChanges, TodoPersistentChanges>();

            var loggerFactory = new LoggerFactory()
                    .AddDebug((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name))
                    .AddConsole((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name));

            // AddDbContext is created as scoped so that it is the same DbContext for the whole request. 
            // For higher performance see DbContext pooling.  https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-2.0
            // Unit Testing: DbContext doesn't have an interface to mock however we do have interfaces for the repositories that can be mocked but  
            //               setting the DbContext to UseInMemory is also and option when unit testing. 
            services.AddDbContext<TodoDbContext>(options =>
            {
                options.UseLoggerFactory(loggerFactory);
                options.UseSqlite(new SqliteConnection("DataSource=Project.db"), b => b.MigrationsAssembly("Cln.Web.Api")); // Move somewhere else. 
            });

            mvcBuilder.AddApplicationPart(Assembly.GetAssembly(typeof(TodoStartup))).AddControllersAsServices();
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }

        public static Assembly GetAssembly()
        {
            return Assembly.GetAssembly(typeof(TodoStartup));
        }
    }
}
