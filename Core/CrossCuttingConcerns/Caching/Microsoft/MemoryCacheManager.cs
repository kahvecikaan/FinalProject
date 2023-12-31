using System.Reflection;
using System.Text.RegularExpressions;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft;

public class MemoryCacheManager : ICacheManager
{
    // why not just use .net core memory cache? because we want to use our own interface
    // adapter pattern
    private readonly IMemoryCache _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>()!;

    public T? Get<T>(string key)
    {
        return _memoryCache.Get<T>(key);
    }

    public object? Get(string key)
    {
        return _memoryCache.Get(key);
    }

    public void Add(string key, object value, int duration)
    {
        _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
    }

    public bool IsAdd(string key)
    {
        return _memoryCache.TryGetValue(key, out _);
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }

    public void RemoveByPattern(string pattern)
    {
        var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", 
            BindingFlags.NonPublic | BindingFlags.Instance);
        var cacheEntriesCollection = cacheEntriesCollectionDefinition?.GetValue(_memoryCache) as dynamic;
        List<ICacheEntry> cacheCollectionValues = new();
        foreach (var cacheItem in cacheEntriesCollection)
        {
            ICacheEntry? cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
            cacheCollectionValues.Add(cacheItemValue);
        }

        var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString()))
            .Select(d => d.Key).ToList();
        foreach (var key in keysToRemove)
        {
            _memoryCache.Remove(key);
        }
    }
}