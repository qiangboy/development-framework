namespace BookStore.Books
{
    public static class BookCacheConsts
    {
        public static class CachePrefix
        {
            public const string Book = "Book";
        }

        public static class CacheKey
        {
            public const string Key_GetList = "Book:GetList-{0}-{1}";
        }
    }
}
