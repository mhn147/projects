using HyperShorts.Web.Data;
using System.Text;

namespace HyperShorts.Web.Core;

public class HyperShortsService(HyperShortsRepository repository)
{
    private readonly HyperShortsRepository _repo = repository;

    public async Task<string> ShortenLongUrl(string longUrl) 
    {
        var hyperShort = new HyperShort
        {
            LongUrl = longUrl
        };

        var savedHyperShort = await _repo.Save(hyperShort);

        savedHyperShort.Code = base62Encode(savedHyperShort.Id);

        await _repo.Update();

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
