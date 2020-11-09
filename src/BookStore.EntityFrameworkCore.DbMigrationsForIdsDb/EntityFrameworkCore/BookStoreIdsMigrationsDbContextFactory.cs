using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookStore.DbMigrationsForIdsDb.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
    public class BookStoreIdsMigrationsDbContextFactory
        : IDesignTimeDbContextFactory<BookStoreIdsMigrationsDbContext>
    {
        public BookStoreIdsMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<BookStoreIdsMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("AbpIdentityServer"));

            return new BookStoreIdsMigrationsDbContext(builder.Options);
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
