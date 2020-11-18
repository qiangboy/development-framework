namespace BookStore
{
    /// <summary>
    /// 缓存过期时间策略
    /// </summary>
    public static class CacheStrategy
    {
        /// <summary>
        /// 一天过期24小时
        /// </summary>

        public const int OneDay = 1440;

        /// <summary>
        /// 12小时过期
        /// </summary>

        public const int HalfDay = 720;

        /// <summary>
        /// 8小时过期
        /// </summary>

        public const int EightHours = 480;

        /// <summary>
        /// 5小时过期
        /// </summary>

        public const int FiveHours = 300;

        /// <summary>
        /// 3小时过期
        /// </summary>

        public const int ThreeHours = 180;

        /// <summary>
        /// 2小时过期
        /// </summary>

        public const int TwoHours = 120;

        /// <summary>
        /// 1小时过期
        /// </summary>

        public const int OneHours = 60;

        /// <summary>
        /// 半小时过期
        /// </summary>

        public const int HalfHours = 30;

        /// <summary>
        /// 5分钟过期
        /// </summary>
        public const int FiveMinutes = 5;

        /// <summary>
        /// 1分钟过期
        /// </summary>
        public const int OneMinute = 1;

        /// <summary>
        /// 永不过期
        /// </summary>

        public const int Never = -1;
    }
}
