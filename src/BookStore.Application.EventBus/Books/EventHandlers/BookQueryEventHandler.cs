using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace BookStore.Application.EventBus.Books.EventHandlers
{
    public class BookQueryEventHandler: IDistributedEventHandler<BookEventData>, ITransientDependency
    {
        public async Task HandleEventAsync(BookEventData eventData)
        {
            await Task.Delay(10);

            Console.WriteLine(eventData.Key);
        }
    }
}
