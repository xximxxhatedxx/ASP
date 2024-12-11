using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class AppDbContext : DbContext
{
    public DbSet<LogEntry> LogEntries { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

public class LogEntry
{
    public int Id { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}
