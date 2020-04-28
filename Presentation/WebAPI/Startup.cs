using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using Data.Common;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Todo.Application;
using Todo.Infrastructure;
using Todo.Persistence.Common.StateActions;
using Todo.Persistence.MySql;
using Todo.Persistence.MySql.Options;
using Todo.WebAPI.Docs;
using Todo.WebAPI.Extensions;
using Todo.WebAPI.Handlers;

namespace Todo.WebAPI
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
            AddDataServices(services);
            services.AddApplication();
            services.AddInfrastructure();
            services.AddControllers();

            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    })
                    .AddFluentValidation(options =>
                    {
                        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(new DateTime(2020, 4, 21), 1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("X-Api-Version");
            });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Context accessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Swagger

            services.AddSwaggerGen(options =>
            {
                var description = GetType().Assembly.ReadEmbeddedResource("Todo.WebAPI.Docs.Description.md");

                options.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "Todo Application API",
                    Description = description,
                    Contact = new OpenApiContact
                    {
                        Name = "Robert Breedt",
                        Email = "robbiebreedt@yahoo.com"
                    }
                });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                options.DocumentFilter<TagDescriptionDocumentFilter>();
            });

            #endregion Swagger
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                await ExceptionHandler.Handle(context);
            }));

            AddSwagger(app);

            app.UseRouting();

            app.UseCors("default");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region Methods

        private static void AddSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseReDoc(options =>
            {
                options.RoutePrefix = "docs";
                options.SpecUrl = "/swagger/v1.0/swagger.json";
                options.DocumentTitle = "Todo Application API";

                options.ExpandResponses(string.Empty);
            });
        }

        private static string GetUser(HttpContext httpContext)
        {
            return httpContext?.User?.Identity?.Name ?? "system";
        }

        private static string GetPath(HttpContext httpContext)
        {
            var request = httpContext?.Request;

            return request == null ? "/no-context" : $"{request.Path} [{request.Method}]";
        }

        private void AddDataServices(IServiceCollection services)
        {
            services.AddScoped(provider =>
            {
                var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
                return new AddedStateContext
                {
                    CreatedBy = GetUser(httpContext),
                    CreatedProcess = GetPath(httpContext)
                };
            });

            services.AddScoped(provider =>
            {
                var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
                return new ModifiedStateContext
                {
                    ModifiedBy = GetUser(httpContext),
                    ModifiedProcess = GetPath(httpContext)
                };
            });

            services.AddScoped(provider =>
            {
                var addedContext = provider.GetService<AddedStateContext>();
                var modifiedContext = provider.GetService<ModifiedStateContext>();
                var contextScope = new ContextScope();
                contextScope.StateActions[EntityState.Added] = addedContext.SetCreatedAuditFields;
                contextScope.StateActions[EntityState.Modified] = modifiedContext.SetModifiedAuditFields;

                return contextScope;
            });

            var mySqlOptions = new MySqlOptions();
            Configuration.GetSection("MySql").Bind(mySqlOptions);

            services.AddMySQLPersistence(options =>
            {
                options.Server = mySqlOptions.Server;
                options.Database = mySqlOptions.Database;
                options.Username = mySqlOptions.Username;
                options.Password = mySqlOptions.Password;
                options.Version = mySqlOptions.Version;
            });
        }

        #endregion Methods
    }
}