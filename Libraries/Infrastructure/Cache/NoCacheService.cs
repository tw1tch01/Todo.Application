using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Services.Cache;

namespace Todo.Infrastructure.Cache
{
    public class NoCacheService : ICacheService
    {
        public Task Clear()
        {
            return Task.CompletedTask;
        }

        public Task<bool> Contains(string key)
        {
            return Task.FromResult(false);
        }

        public Task<T> Get<T>(string key, int cacheTime, Func<Task<T>> acquire)
        {
            return Task.FromResult((T)default);
        }

        public Task<IDictionary<string, T>> Lookup<T>(string key)
        {
            return Task.FromResult((IDictionary<string, T>)null);
        }

        public Task Remove(string key)
        {
            return Task.CompletedTask;
        }

        public Task Set<T>(string key, T data, int cacheTime)
        {
            return Task.CompletedTask;
        }

        public Task Update<T>(string key, T data)
        {
            return Task.CompletedTask;
        }
    }
}