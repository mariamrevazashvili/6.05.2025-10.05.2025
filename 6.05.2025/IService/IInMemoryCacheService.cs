namespace _6._05._2025.IService
{
    public interface IInMemoryCacheService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan expirationDuration);
    }
}
