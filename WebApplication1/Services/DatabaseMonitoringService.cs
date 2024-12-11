using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public class DatabaseMonitorService : BackgroundService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<DatabaseMonitorService> _logger;
    private readonly EmailService _emailService;

    public DatabaseMonitorService(AppDbContext dbContext, ILogger<DatabaseMonitorService> logger, EmailService emailService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var lastCheckedTime = DateTime.Now;

        while (!stoppingToken.IsCancellationRequested)
        {
            var newEntries = await _dbContext.LogEntries
                .Where(entry => entry.Timestamp > lastCheckedTime)
                .ToListAsync(stoppingToken);

            foreach (var entry in newEntries)
            {
                _logger.LogInformation($"New DB Entry: {entry.Message}");

                try
                {
                    var subject = "New DB Log Entry";
                    var body = $"New Log Entry:\n\nMessage: {entry.Message}\nTimestamp: {entry.Timestamp}";
                    await _emailService.SendEmailAsync(subject, body);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sending email notification.");
                }
            }

            lastCheckedTime = DateTime.Now;

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
