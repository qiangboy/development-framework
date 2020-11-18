using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BookStore.Books
{
    public interface IBookAppCacheService
    {
        Task<PagedResultDto<BookDto>> GetListAsync(PagedAndSortedResultRequestDto input, Func<Task<PagedResultDto<BookDto>>> factory);
    }
}
