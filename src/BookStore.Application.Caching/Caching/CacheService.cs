using System;
using System.Threading.Tasks;
using BookStore.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Application.Dtos;

namespace BookStore.Application.Caching.Caching
{
    public class CacheService<TCacheItem> : ICacheService<TCacheItem>
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<TCacheItem> GetAsync(string key, Func<Task<TCacheItem>> factory)
        {
            return await _distributedCache.GetOrAddAsync(key, factory);
        }

        public async Task<TAnyCacheItem> GetAsync<TAnyCacheItem>(string key, Func<Task<TAnyCacheItem>> factory)
        {
            return await _distributedCache.GetOrAddAsync(key, factory);
        }

        public async Task<PagedResultDto<TCacheItem>> GetListAsync(string key, Func<Task<PagedResultDto<TCacheItem>>> factory)
        {
            return await _distributedCache.GetOrAddAsync(key, factory);
        }

        public async Task RemoveAsync(string cachePrefix, int cursor = 0)
        {
            var keys = await RedisHelper.KeysAsync($"{cachePrefix}*");

            await RedisHelper.DelAsync(keys);
        }
    }
}
