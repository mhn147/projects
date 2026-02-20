using HyperShorts.Web.Data;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace HyperShorts.Web.Core;

public class HyperShortsService(HyperShortsRepository repository, HyperShortsCache cache)
{
    private readonly HyperShortsRepository _repo = repository;
    private readonly HyperShortsCache _cache = cache;

    public async Task<string?> GetLongUrl(string code)
    {
        var longUrl = await _cache.GetLongUrl(code);
        if (!string.IsNullOrWhiteSpace(longUrl))
        {
            return longUrl;
        }

        var hyperShort = await _repo.Get(code);

        if (hyperShort != null)
        {
            await _cache.Set(hyperShort.Code, hyperShort.LongUrl);
        }

        return hyperShort?.LongUrl;
    }

    public async Task<string> ShortenLongUrl(string longUrl)
    {
        var hyperShort = new HyperShort
        {
            LongUrl = longUrl
        };

        var savedHyperShort = await _repo.Save(hyperShort);

        savedHyperShort.Code = base62Encode(savedHyperShort.Id);

        await _repo.Update();

        await _cache.Set(savedHyperShort.Code, savedHyperShort.LongUrl);

        return savedHyperShort.Code;
    }

    private string base62Encode(long value)
    {
        const string alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be non-negative.");

        if (value == 0)
            return alphabet[0].ToString();

        var sb = new StringBuilder();

        while (value > 0)
        {
            int remainder = (int)(value % 62);
            sb.Insert(0, alphabet[remainder]);
            value /= 62;
        }

        return sb.ToString();
    }
}
