using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookStore.DbMigrationsForIdsDb.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
    public class BookStoreMigrationsIdsDbContextFactory
        : IDesignTimeDbContextFactory<BookStoreMigrationsIdsDbContext>
    {
        public BookStoreMigrationsIdsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<BookStoreMigrationsIdsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("AbpIdentityServer"));

            return new BookStoreMigrationsIdsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }

}
