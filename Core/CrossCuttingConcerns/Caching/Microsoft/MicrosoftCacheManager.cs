
using Microsoft.Extensions.Caching.Memory;

namespace Core.CrossCuttingConcerns.Caching.Microsoft;

public class MicrosoftCacheManager(IMemoryCache memoryCache) : ICacheService
{
    private List<string> _keys = [];
    public void Add(string key, object value, int durationMinute)
    {
        memoryCache.Set(key, value, TimeSpan.FromMinutes(durationMinute));
        _keys.Add(key);
    }

    public object Get(string key)
    {
        if (IsAdd(key))
            return memoryCache.Get(key);

        return default;
    }

    public T Get<T>(string key)
    {
        if (memoryCache.TryGetValue(key, out var value) && value != null)
            return (T)value;

        return default;
    }

    public void Remove(string key)
    {
        memoryCache.Remove(key);
        _keys.Remove(key);
    }

    public bool IsAdd(string key) => memoryCache.TryGetValue(key, out _);

    public IEnumerable<string> GetKeys()
    {
        return _keys;
    }
}
