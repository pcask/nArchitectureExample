
namespace Core.CrossCuttingConcerns.Caching.Custom;

public static class CacheStore
{
    public static IDictionary<string, (object, DateTime)> Datas { get; set; }
}

public class CustomCacheManager : ICacheService
{
    public void Add(string key, object value, int durationMin = 30)
    {
        if (IsAdd(key))
        {
            CacheStore.Datas[key] = (value, DateTime.UtcNow.AddMinutes(durationMin));
            return;
        }

        CacheStore.Datas.Add(key, (value, DateTime.UtcNow.AddMinutes(durationMin)));
    }

    public object Get(string key)
    {
        if (IsAdd(key))
            return CacheStore.Datas[key];

        return default;
    }

    public T Get<T>(string key)
    {
        if (IsAdd(key))
            return (T)CacheStore.Datas[key].Item1;

        return default;
    }

    public void Remove(string key)
    {
        if (IsAdd(key))
            CacheStore.Datas.Remove(key);
    }
    public bool IsAdd(string key)
    {
        RemoveExpiredCaches();
        return CacheStore.Datas.ContainsKey(key);
    }

    private void RemoveExpiredCaches()
    {
        CacheStore.Datas.Where(c => c.Value.Item2 < DateTime.UtcNow).Select(c => CacheStore.Datas.Remove(c.Key));
    }

    public IEnumerable<string> GetKeys()
    {

        RemoveExpiredCaches();
        return CacheStore.Datas.Keys;
    }
}
