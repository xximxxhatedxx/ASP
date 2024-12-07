using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.WithExceptionDetails()
    .WriteTo.Console() 
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Minute)
    .WriteTo.Email(
        from: "dhficienfnicjendhcudb@gmail.com",
        to: "dhficienfnicjendhcudb@gmail.com",
        host: "smtp.gmail.com",
        port: 587,
        connectionSecurity: MailKit.Security.SecureSocketOptions.StartTls,
        credentials: new NetworkCredential(
          "dhficienfnicjendhcudb@gmail.com", "pbhy dqxj licx amil"))
    .Destructure.ByTransforming<Exception>(e => new { e.Message, e.StackTrace })
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();