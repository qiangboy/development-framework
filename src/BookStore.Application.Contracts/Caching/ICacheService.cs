using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BookStore.Caching
{
    public interface ICacheService<TCacheItem>
    {
        Task<TCacheItem> GetAsync(string key, Func<Task<TCacheItem>> factory);

        Task<TAnyCacheItem> GetAsync<TAnyCacheItem>(string key, Func<Task<TAnyCacheItem>> factory);

        Task<PagedResultDto<TCacheItem>> GetListAsync(string key, Func<Task<PagedResultDto<TCacheItem>>> factory);

        Task RemoveAsync(string cachePrefix, int cursor = 0);
    }
}
