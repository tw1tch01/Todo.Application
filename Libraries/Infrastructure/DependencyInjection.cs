﻿using Microsoft.Extensions.DependencyInjection;

namespace Todo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //services.AddTransient<ICacheService, NoCacheService>();

            return services;
        }
    }
}