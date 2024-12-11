using Microsoft.Extensions.Caching.Memory;

public class ApiDataCacher : BackgroundService
{
    private readonly IMemoryCache _cache;
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiDataCacher(IMemoryCache cache, IHttpClientFactory httpClientFactory)
    {
        _cache = cache;
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync("https://api.exchangeratesapi.io/latest");

            _cache.Set("ExchangeRates", response, TimeSpan.FromMinutes(10));
            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }
    }
}