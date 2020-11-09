using BookStore.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace BookStore.DbMigrationsForIdsDb.EntityFrameworkCore
{
    [DependsOn(
        typeof(BookStoreEntityFrameworkCoreModule)
    )]
    public class BookStoreEntityFrameworkCoreIdsDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<BookStoreIdsMigrationsDbContext>();
        }
    }
}
