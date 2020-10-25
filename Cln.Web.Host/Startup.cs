using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cln.Application.Todo.Models;
using Cln.Controllers.Todo.Bootstrap;
using Cln.Web.Bootstrap;

namespace Cln.Web.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mvc = ApplicationStartup.ConfigureServices(services);

            TodoStartup.ConfigureServices(services, mvc);

            var allMappingProfiles = new[] { Assembly.GetAssembly(typeof(TodoMappingProfile)) };

            ApplicationStartup.ConfigureModuleServices(services, allMappingProfiles);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ApplicationStartup.Configure(app, env);
        }
    }
}