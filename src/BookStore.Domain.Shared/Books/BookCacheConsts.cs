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
            public const string Key_Get = CachePrefix.Book + ":{0}";

            public const string Key_GetList = CachePrefix.Book + ":GetList-{0}-{1}";

            public const string Key_AuthorLookup = CachePrefix.Book + ":AuthorLookup";
        }
    }
}
