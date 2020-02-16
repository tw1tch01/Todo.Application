using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Todo.Persistence.MySQL.Options;
using Todo.Services.Common;

namespace Todo.Persistence.MySQL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMySQLPersistence(this IServiceCollection services, Action<MySqlOptions> mySqlOptions)
        {
            var options = new MySqlOptions();
            mySqlOptions(options);

            services.AddTransient<ITodoContext, TodoContext>();

            services.AddDbContext<TodoContext>(opt => opt
                .UseMySql($"Server={options.Server};Database={options.Database};User={options.Username};Password={options.Password};",
                contextOptions =>
                {
                    contextOptions.ServerVersion(new Version(options.Version.Major, options.Version.Minor, options.Version.Build), ServerType.MySql);
                    contextOptions.MigrationsAssembly("Todo.Persistence.MySQL");
                }), ServiceLifetime.Transient);

            return services;
        }
    }
}