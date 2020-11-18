using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace BookStore.Application.EventBus.Books.EventHandlers
{
    public class BookCachingRemoveEventHandler: IDistributedEventHandler<BookCachingRemoveEventData>, ITransientDependency
    {
        private readonly IDistributedCache _distributedCache;

        public BookCachingRemoveEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task HandleEventAsync(BookCachingRemoveEventData eventData)
        {
            await _distributedCache.RemoveAsync(eventData.Key);
        }
    }
}
