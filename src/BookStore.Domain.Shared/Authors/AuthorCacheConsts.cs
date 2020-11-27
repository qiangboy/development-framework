namespace BookStore.Authors
{
    public static class AuthorCacheConsts
    {
        public static class CachePrefix
        {
            public const string Author = "Author";
        }

        public static class CacheKey
        {
            public const string Key_Get = CachePrefix.Author + ":{0}";

            public const string Key_GetList = CachePrefix.Author + ":GetList-{0}-{1}";
        }
    }
}
