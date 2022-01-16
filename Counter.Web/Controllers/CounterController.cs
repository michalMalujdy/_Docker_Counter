using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Counter.Web.Controllers;

[Route("/")]
public class CounterController : ControllerBase
{
    private readonly IDistributedCache _cache;

    private const string CounterCacheKey = "counter";

    public CounterController(IDistributedCache cache)
    {
        _cache = cache;
    }

    [HttpGet]
    public async Task<string> GetCounter()
    {
        var counter = await _cache.GetStringAsync(CounterCacheKey);

        counter = string.IsNullOrEmpty(counter)
            ? "1"
            : (int.Parse(counter) + 1).ToString();

        await _cache.SetStringAsync(CounterCacheKey, counter);

        return $"The page was visited {counter} times";
    }
}