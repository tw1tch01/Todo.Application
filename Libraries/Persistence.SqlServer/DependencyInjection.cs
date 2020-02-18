using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.SqlServer.Options;
using Todo.Persistence.Common;
using Todo.Services.Common;

namespace Todo.Persistence.SqlServer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSqlServerPersistence(this IServiceCollection services, Action<SqlServerOptions> sqlServerOptions)
        {
            var options = new SqlServerOptions();
            sqlServerOptions(options);

            services.AddTransient<ITodoContext, TodoContext>();

            services.AddDbContext<TodoContext>(opt => opt
                .UseSqlServer(options.ConnectionString, contextOptions =>
                {
                    contextOptions.MigrationsAssembly("Todo.Persistence.Common");
                }), ServiceLifetime.Transient);

            return services;
        }
    }
}