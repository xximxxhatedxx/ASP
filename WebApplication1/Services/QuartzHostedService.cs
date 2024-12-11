using Quartz.Spi;
using Quartz;

public class QuartzHostedService : IHostedService
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IServiceProvider _serviceProvider;
    private IScheduler _scheduler;

    public QuartzHostedService(ISchedulerFactory schedulerFactory, IServiceProvider serviceProvider)
    {
        _schedulerFactory = schedulerFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);

        _scheduler.JobFactory = new ScopedJobFactory(_serviceProvider);

        var job = JobBuilder.Create<QuartzJob>()
            .WithIdentity("LogEntryJob", "Group1")
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("LogEntryTrigger", "Group1")
            .StartNow()
            .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
            .Build();

        await _scheduler.ScheduleJob(job, trigger, cancellationToken);
        await _scheduler.Start(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_scheduler != null)
        {
            await _scheduler.Shutdown(cancellationToken);
        }
    }
}

public class QuartzJob : IJob
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<QuartzJob> _logger;

    public QuartzJob(AppDbContext dbContext, ILogger<QuartzJob> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var newLogEntry = new LogEntry
            {
                Message = "New log entry created by Quartz job",
                Timestamp = DateTime.Now
            };
            _dbContext.Database.EnsureCreated();
            _dbContext.LogEntries.Add(newLogEntry);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("New log entry added to the database: {Message}", newLogEntry.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding a log entry to the database.");
        }
    }
}

public class ScopedJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ScopedJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        var scope = _serviceProvider.CreateScope();
        var job = scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        return job;
    }

    public void ReturnJob(IJob job)
    {
    }
}
