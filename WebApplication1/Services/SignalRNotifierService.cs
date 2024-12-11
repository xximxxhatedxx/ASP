using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System;
using Microsoft.Extensions.Caching.Memory;

public class SignalRNotifierService : BackgroundService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IMemoryCache _memoryCache;

    public SignalRNotifierService(IHubContext<NotificationHub> hubContext, IMemoryCache memoryCache)
    {
        _hubContext = hubContext;
        _memoryCache = memoryCache;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var cacheValue = _memoryCache.Get("ExchangeRates");
            if (cacheValue != null)
            {
                _hubContext.Clients.All.SendAsync("ExchangeRates", cacheValue);
            }
            else
            {
                _hubContext.Clients.All.SendAsync("ExchangeRates", "Cache is empty.");
            }
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Server message", stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}

public class NotificationHub : Hub
{
}

//if (typeof signalR === "undefined")
//{
//    var script = document.createElement("script");
//    script.src = "https://cdn.jsdelivr.net/npm/@microsoft/signalr@7.0.0/dist/browser/signalr.js";
//    document.head.appendChild(script);
//    script.onload = function() {
//        startSignalR();
//    };
//}
//else
//{
//    startSignalR();
//}

//function startSignalR()
//{
//    const connection = new signalR.HubConnectionBuilder()
//        .withUrl("/notifications")
//        .build();

//    connection.on("ReceiveMessage", function(message) {
//        console.log("Message:", message);
//    });

//    connection.on("ExchangeRates", function(cacheData) {
//        console.log("Cache Data:", cacheData);
//    });

//    connection.start().then(function() {
//        console.log("Connected.");
//    }).catch(function(err) {
//        console.error("Error:", err.toString());
//    });
//}