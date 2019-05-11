using System.Reflection;
using Cln.Application.Todo.Models;
using Cln.Controllers.Todo.Bootstrap;
using Cln.Web.Bootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Debug.Web.Api.Todo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mvc = ApplicationStartup.ConfigureServices(services);

            TodoStartup.ConfigureServices(services, mvc);

            var allMappingProfiles = new[] { Assembly.GetAssembly(typeof(TodoMappingProfile)) };

            ApplicationStartup.ConfigureModuleServices(services, allMappingProfiles);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ApplicationStartup.Configure(app, env);

            var apiControllers = new[] { TodoStartup.GetAssembly() };

            ApplicationStartup.ConfigureModules(app, env, apiControllers);
        }
    }
}
