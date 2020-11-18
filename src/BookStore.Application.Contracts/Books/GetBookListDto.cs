using Volo.Abp.Application.Dtos;

namespace BookStore.Books
{
    public class GetBookListDto : PagedAndSortedResultRequestDto
    {
        public string  Filter { get; set; }
    }
}
