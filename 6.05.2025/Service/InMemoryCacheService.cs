using _6._05._2025.IService;

namespace _6._05._2025.Service
{
    // 6.05.2025 && 10.05.2025
    public class InMemoryCacheService : IInMemoryCacheService
    {
        private static InMemoryCacheService _unique;
        private class CacheItem
        {
            public object Value { get; set; }
            public DateTime Expiration { get; set; }
        }

        private Dictionary<string, CacheItem> _cache = new();

        private InMemoryCacheService()
        {
            _cache = new Dictionary<string, CacheItem>();
        }

        public static InMemoryCacheService GetInstance()
        {
            if (_unique == null)
            {
                _unique = new InMemoryCacheService();
            }

            return _unique;
        }

        public T Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out var item))
            {
                if (DateTime.UtcNow <= item.Expiration)
                {
                    return (T)item.Value;
                }
                else
                {
                    _cache.Remove(key);
                }
            }

            return default;
        }

        public void Set<T>(string key, T value, TimeSpan expirationDuration)
        {
            var item = new CacheItem
            {
                Value = value,
                Expiration = DateTime.UtcNow.Add(expirationDuration)
            };

            _cache[key] = item;
        }
    }
}
