using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Cln.Api.Handlers.Authorization;
using Cln.Application.Interfaces.Projects;
using Cln.Infrastructure.Projects;
using Cln.Web.Api.Filters;
using Cln.Web.Filters.ExceptionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace Cln.Web.Bootstrap
{
    public static class ApplicationStartup
    {
        public static IMvcBuilder ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins("http://localhost")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            });

            var mvcBuilder = services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ProjectAuthorizationFilter)); // Applies to all controllers.
                options.Filters.Add(typeof(ValidateModelFilter)); // Applies to all controller
                options.Filters.Add(typeof(ExceptionToStatusCodeFilter)); // Applies to all controller
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddAuthorization(options => { options.AddPolicy("TodoAccessPolicy", policy => policy.Requirements.Add(new ProjectAccessRequirement())); });
            services.AddSingleton<IAuthorizationHandler, ProjectAuthorizationHandler>();

            services.AddDbContext<ProjectDbContext>(options => { options.UseSqlite(new SqliteConnection("DataSource=Project.db")); });
            services.AddScoped<IProjectRepository, ProjectDbRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Version = "v1" });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"Cln.Controllers.Todo.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"Cln.Application.Todo.xml"));

                c.CustomSchemaIds(classInfo =>
                {
                    string returnedValue = classInfo.Name;

                    if (returnedValue.EndsWith("Model"))
                        returnedValue = returnedValue.Replace("Model", string.Empty);

                    return returnedValue;
                });
            });

            return mvcBuilder;
        }

        public static void ConfigureModuleServices(IServiceCollection services, Assembly[] mapperProfileAssemblies)
        {
            services.AddAutoMapper(mapperProfileAssemblies);
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
            });
        }
    }
}
