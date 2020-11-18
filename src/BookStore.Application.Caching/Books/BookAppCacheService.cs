using System;
using System.Threading.Tasks;
using BookStore.Books;
using BookStore.ToolKits.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace BookStore.Application.Caching.Books
{
    public class BookAppCacheService : IBookAppCacheService, ITransientDependency
    {
        private readonly IDistributedCache _distributedCache;

        public BookAppCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<PagedResultDto<BookDto>> GetListAsync(PagedAndSortedResultRequestDto input, Func<Task<PagedResultDto<BookDto>>> factory)
        {
            return await _distributedCache.GetOrAddAsync(
                BookCacheConsts.CacheKey.Key_GetList.FormatWith(input.SkipCount, input.MaxResultCount),
                factory,
                CacheStrategy.OneDay);
        }
    }
}
