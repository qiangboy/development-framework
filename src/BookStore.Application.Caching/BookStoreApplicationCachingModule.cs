using BookStore.Application.EventBus;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace BookStore.Application.Caching
{
    [DependsOn(
        typeof(AbpCachingModule),
        typeof(BookStoreApplicationContractsModule),
        typeof(BookStoreDomainModule),
        typeof(BookStoreApplicationEventBusModule)
        )]
    public class BookStoreApplicationCachingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
        }
    }
}
