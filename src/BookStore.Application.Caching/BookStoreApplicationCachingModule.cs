using BookStore.Application.Caching.Caching;
using BookStore.Application.EventBus;
using BookStore.Caching;
using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
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
            context.Services.AddTransient(typeof(ICacheService<>),typeof(CacheService<>));

            context.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = context.Services.GetConfiguration()["Redis:Configuration"];
            });

            var csRedis = new CSRedisClient(context.Services.GetConfiguration()["Redis:Configuration"]);
            RedisHelper.Initialization(csRedis);

            context.Services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));
        }
    }
}
