using System.Diagnostics;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<EmailService>();
builder.Services.AddSignalR();
builder.Services.AddHostedService<PageAvailabilityChecker>();
builder.Services.AddScoped<DatabaseMonitorService>();
builder.Services.AddHostedService(provider =>
{
    var scope = provider.CreateScope();
    return scope.ServiceProvider.GetRequiredService<DatabaseMonitorService>();
});

builder.Services.AddHostedService<ApiDataCacher>();
builder.Services.AddHostedService<SignalRNotifierService>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddScoped<ScopedJobFactory>();
builder.Services.AddScoped<QuartzJob>();
builder.Services.AddHostedService<QuartzHostedService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WebApplication2Db;Trusted_Connection=True;"));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapHub<NotificationHub>("/notifications");
});

app.Run();
