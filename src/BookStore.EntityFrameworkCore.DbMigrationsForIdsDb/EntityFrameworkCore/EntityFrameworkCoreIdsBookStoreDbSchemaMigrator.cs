using System;
using System.Threading.Tasks;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace BookStore.DbMigrationsForIdsDb.EntityFrameworkCore
{
    public class EntityFrameworkCoreIdsBookStoreDbSchemaMigrator
        : IBookStoreDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreIdsBookStoreDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the BookStoreIdsMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<BookStoreIdsMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
