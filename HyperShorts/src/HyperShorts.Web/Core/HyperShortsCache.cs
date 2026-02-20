using Microsoft.Extensions.Caching.Distributed;

namespace HyperShorts.Web.Core;

public class HyperShortsCache(IDistributedCache cache, ILogger<HyperShortsCache> logger)
{
    private readonly IDistributedCache _cache = cache;
    private readonly ILogger<HyperShortsCache> _logger = logger;

    private static string Key(string code) => $"url:{code}";

    public async Task<string?> GetLongUrl(string code)
    {
        try
        {
            return await _cache.GetStringAsync(Key(code));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task Set(string? code, string? longUrl)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(code);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(longUrl);

        try
        {
            await _cache.SetStringAsync(
                Key(code),
                longUrl,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6),
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
