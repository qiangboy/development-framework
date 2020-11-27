using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Application.EventBus.Books;
using BookStore.Caching;
using BookStore.Permissions;
using BookStore.ToolKits.Extensions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Distributed;

namespace BookStore.Authors
{
    [Authorize(BookStorePermissions.Authors.Default)] // 需要授权
    public class AuthorAppService : ApplicationService, IAuthorAppService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly ICacheService<AuthorDto> _cacheService;

        public AuthorAppService(
            IAuthorRepository authorRepository,
            AuthorManager authorManager,
            IDistributedEventBus distributedEventBus, 
            ICacheService<AuthorDto> cacheService)
        {
            _authorRepository = authorRepository;
            _authorManager = authorManager;
            _distributedEventBus = distributedEventBus;
            _cacheService = cacheService;
        }

        public async Task<AuthorDto> GetAsync(Guid id)
        {
            return await _cacheService.GetAsync(AuthorCacheConsts.CacheKey.Key_Get.FormatWith(id), async () =>
            {
                var author = await _authorRepository.GetAsync(id);

                return ObjectMapper.Map<Author, AuthorDto>(author);
            });
        }

        public async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input)
        {
            return await _cacheService.GetListAsync(AuthorCacheConsts.CacheKey.Key_GetList.FormatWith(input.SkipCount, input.MaxResultCount), async () =>
             {
                 if (string.IsNullOrWhiteSpace(input.Sorting))
                 {
                     input.Sorting = nameof(Author.Name);
                 }

                 var authors = await _authorRepository.GetListAsync(
                     input.SkipCount,
                     input.MaxResultCount,
                     input.Sorting,
                     input.Filter
                 );

                 var totalCount = await AsyncExecuter.CountAsync(
                     _authorRepository.WhereIf(
                         !string.IsNullOrWhiteSpace(input.Filter),
                         author => author.Name.Contains(input.Filter)
                     )
                 );

                 return new PagedResultDto<AuthorDto>(
                     totalCount,
                     ObjectMapper.Map<List<Author>, List<AuthorDto>>(authors)
                 );
             });
        }

        public async Task<PagedResultDto<AuthorDto>> GetDeletedListAsync()
        {
            var query = _authorRepository.AsQueryable();

            var totalCount = await AsyncExecuter.CountAsync(query.Where(x => x.IsDeleted));

            var deletedAuthors = await AsyncExecuter.ToListAsync(query.Where(x => x.IsDeleted));

            return new PagedResultDto<AuthorDto>(
                totalCount,
                ObjectMapper.Map<List<Author>,List<AuthorDto>>(deletedAuthors));
        }

        [Authorize(BookStorePermissions.Authors.Create)]
        public async Task<AuthorDto> CreateAsync(CreateAuthorDto input)
        {
            var author = await _authorManager.CreateAsync(
                input.Name,
                input.BirthDate,
                input.ShortBio
            );

            await _authorRepository.InsertAsync(author);

            await _cacheService.RemoveAsync(AuthorCacheConsts.CachePrefix.Author);

            await _distributedEventBus.PublishAsync(new BookEventData
            {
                Key = AuthorCacheConsts.CachePrefix.Author
            });

            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        [Authorize(BookStorePermissions.Authors.Edit)]
        public async Task UpdateAsync(Guid id, UpdateAuthorDto input)
        {
            var author = await _authorRepository.GetAsync(id);

            if (author.Name != input.Name)
            {
                await _authorManager.ChangeNameAsync(author, input.Name);
            }

            author.BirthDate = input.BirthDate;
            author.ShortBio = input.ShortBio;

            await _authorRepository.UpdateAsync(author);

            await _cacheService.RemoveAsync(AuthorCacheConsts.CachePrefix.Author);

            await _distributedEventBus.PublishAsync(new BookEventData
            {
                Key = AuthorCacheConsts.CachePrefix.Author
            });
        }

        [Authorize(BookStorePermissions.Authors.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _authorRepository.DeleteAsync(id);

            await _cacheService.RemoveAsync(AuthorCacheConsts.CachePrefix.Author);

            await _distributedEventBus.PublishAsync(new BookEventData
            {
                Key = AuthorCacheConsts.CachePrefix.Author
            });
        }
    }
}
