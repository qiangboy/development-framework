using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Application.EventBus.Books;
using BookStore.Authors;
using BookStore.Caching;
using BookStore.Permissions;
using BookStore.ToolKits.Extensions;
using BookStore.Volo.Abp.Emailing;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;

namespace BookStore.Books
{
    [Authorize(BookStorePermissions.Books.Default)]
    public class BookAppService : CrudAppService<Book,BookDto,Guid,PagedAndSortedResultRequestDto,CreateUpdateBookDto>, IBookAppService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly EmailingTextTemplateService _emailingTextTemplateService;
        private readonly ICacheService<BookDto> _cacheService;

        public BookAppService(
            IRepository<Book, Guid> repository,
            IAuthorRepository authorRepository,
            IDistributedEventBus distributedEventBus,
            EmailingTextTemplateService emailingTextTemplateService,
            ICacheService<BookDto> cacheService) 
            : base(repository)
        {
            _authorRepository = authorRepository;
            _distributedEventBus = distributedEventBus;
            _emailingTextTemplateService = emailingTextTemplateService;
            _cacheService = cacheService;
            GetPolicyName = BookStorePermissions.Books.Default;
            GetListPolicyName = BookStorePermissions.Books.Default;
            CreatePolicyName = BookStorePermissions.Books.Create;
            UpdatePolicyName = BookStorePermissions.Books.Edit;
            DeletePolicyName = BookStorePermissions.Books.Delete;
        }

        /// <summary>
        /// 重写GetAsync，获取作者信息，把作者名添加到BookDto中
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<BookDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            return await _cacheService.GetAsync(BookCacheConsts.CacheKey.Key_Get.FormatWith(id), async () =>
            {
                //Prepare a query to join books and authors
                var query = from book in Repository
                    join author in _authorRepository on book.AuthorId equals author.Id
                    where book.Id == id
                    select new {book, author};

                //Execute the query and get the book with author
                var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
                if (queryResult == null)
                {
                    throw new EntityNotFoundException(typeof(Book), id);
                }

                var bookDto = ObjectMapper.Map<Book, BookDto>(queryResult.book);
                bookDto.AuthorName = queryResult.author.Name;
                return bookDto;
            });
        }

        public override async Task<PagedResultDto<BookDto>>
            GetListAsync(PagedAndSortedResultRequestDto input)
        {
            await CheckGetListPolicyAsync(); // 检查查询该结果需要的策略

            return await _cacheService.GetListAsync(BookCacheConsts.CacheKey.Key_GetList.FormatWith(input.SkipCount, input.MaxResultCount), async () =>
            {
                //Prepare a query to join books and authors
                var query = from book in Repository
                    join author in _authorRepository on book.AuthorId equals author.Id
                    orderby input.Sorting
                    select new { book, author };

                query = query
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

                //Execute the query and get a list
                var queryResult = await AsyncExecuter.ToListAsync(query);

                //Convert the query result to a list of BookDto objects
                var bookDtos = queryResult.Select(x =>
                {
                    var bookDto = ObjectMapper.Map<Book, BookDto>(x.book);
                    bookDto.AuthorName = x.author.Name;
                    return bookDto;
                }).ToList();

                //Get the total count with another query
                var totalCount = await Repository.GetCountAsync();

                return new PagedResultDto<BookDto>(
                    totalCount,
                    bookDtos
                );
            });
        }

        public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
        {
            return await _cacheService.GetAsync(BookCacheConsts.CacheKey.Key_AuthorLookup, async () =>
            {
                var authors = await _authorRepository.GetListAsync();

                return new ListResultDto<AuthorLookupDto>(
                    ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
                );
            });
        }

        //[IgnoreAntiforgeryToken]
        public async Task<string> TestRemoveCache()
        {
            await _distributedEventBus.PublishAsync(new BookEventData
            {
                Key = ""
            });
            return await _emailingTextTemplateService.RunAsync();
        }
    }
}
