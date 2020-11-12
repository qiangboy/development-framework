using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace BookStore.DbMigrationsForIdsDb.EntityFrameworkCore
{
    [ConnectionStringName("AbpIdentityServer")]
    public class BookStoreMigrationsIdsDbContext :
        AbpDbContext<BookStoreMigrationsIdsDbContext>
    {
        public BookStoreMigrationsIdsDbContext(
            DbContextOptions<BookStoreMigrationsIdsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigureIdentityServer();
        }
    }

}
