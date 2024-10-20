namespace RedisAcceleratedAPI.API.Helper
{
    public static class AppConstants
    {
        public static class CacheKeys
        {
            public const string Products = "products";
        }


        public static class CacheDuration
        {
            public const int ProductsCacheMinutes = 10;
        }

        public static class MessageKeys
        {
            public const string ID_MISMATCH = "ID mismatch";
            public const string PRODUCT_NOT_FOUND = "Product not found";
            public const string PRODUCT_DELETED = "Product deleted successfully";
        }
    }
}
