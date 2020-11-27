using BookStore.Application.Caching;
using BookStore.Application.EventBus;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace BookStore
{
    [DependsOn(
        typeof(BookStoreDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(BookStoreApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(BookStoreApplicationEventBusModule),
        typeof(BookStoreApplicationCachingModule)
    )]
    public class BookStoreApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BookStoreApplicationModule>();
            });
        }
    }
}
