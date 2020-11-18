using System;
using System.Threading.Tasks;
using BookStore.ToolKits.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace BookStore.Application.Caching
{
    public static class BookStoreApplicationCachingExtensions
    {
        /// <summary>
        /// 获取或添加缓存
        /// </summary>
        /// <typeparam name="TCacheItem">缓存项</typeparam>
        /// <param name="cache">缓存对象</param>
        /// <param name="key">键</param>
        /// <param name="factory">数据源委托</param>
        /// <param name="minutes">过期时间（分钟）</param>
        /// <returns></returns>
        public static async Task<TCacheItem> GetOrAddAsync<TCacheItem>(this IDistributedCache cache, string key, Func<Task<TCacheItem>> factory, int minutes = CacheStrategy.OneDay)
        {
            if(string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            TCacheItem cacheItem;

            var result = await cache.GetStringAsync(key);
            if (!string.IsNullOrWhiteSpace(result))
            {
                cacheItem = result.FromJson<TCacheItem>();
            }
            else
            {
                cacheItem = await factory.Invoke();

                var options = new DistributedCacheEntryOptions();
                if (minutes != CacheStrategy.Never)
                {
                    options.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(minutes);
                }

                await cache.SetStringAsync(key, cacheItem.ToJson(), options);
            }

            return cacheItem;
        }
    }
}
