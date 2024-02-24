namespace Core.CrossCuttingConcerns.Caching;

public interface ICacheService
{
    void Add(string key, object value, int durationMinute);
    object Get(string key);
    T Get<T>(string key);
    void Remove(string key);
    bool IsAdd(string key);
    IEnumerable<string> GetKeys();
}
