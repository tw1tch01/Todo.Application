using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure.Cache;
using Todo.Services.Cache;

namespace Todo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICacheService, NoCacheService>();

            return services;
        }
    }
}