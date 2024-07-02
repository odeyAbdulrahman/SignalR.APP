using Microsoft.Extensions.Caching.Distributed;

namespace SignalR.API.Services;
public class RedisCacheService : IRedisCache
{
    private readonly IDistributedCache DistributedCache;
    public RedisCacheService(IDistributedCache distributedCache)
    {
        DistributedCache = distributedCache;
    }

    public async Task<string> GetAsync(string? key)
    {
        var types = await DistributedCache.GetStringAsync(key);
        if (string.IsNullOrEmpty(types))
            return string.Empty;
        return types;
    }

    public async Task<string> UpdateAsync(string? key, string? data, int? absoluteExpiration = null, int? slidingExpiration = null)
    {
        await DistributedCache.SetStringAsync(key, data, CacheEntryOptions(absoluteExpiration, slidingExpiration));
        return await GetAsync(key);
    }

    public async Task DeleteAsync(string? key)
    {
        await DistributedCache.RemoveAsync(key);
    }

    private static DistributedCacheEntryOptions CacheEntryOptions(int? absoluteExpiration = null, int? slidingExpiration = null)
    {
        return new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(absoluteExpiration ?? 24),
            SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration ?? 60)
        };
    }
}
