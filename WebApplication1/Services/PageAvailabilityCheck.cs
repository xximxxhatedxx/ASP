public class PageAvailabilityChecker : BackgroundService
{
    private readonly ILogger<PageAvailabilityChecker> _logger;

    public PageAvailabilityChecker(ILogger<PageAvailabilityChecker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync("https://example.com", stoppingToken);
                var status = response.IsSuccessStatusCode ? "Available" : "Unavailable";
                await File.AppendAllTextAsync("log.txt", $"{DateTime.Now}: {status}\n", stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking page availability");
            }

            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }
    }
}