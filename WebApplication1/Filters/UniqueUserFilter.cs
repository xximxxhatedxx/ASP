using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

public class UniqueUserFilter : IActionFilter
{
    private static ConcurrentDictionary<string, bool> _uniqueUsers = new ConcurrentDictionary<string, bool>();
    private readonly string _userCountFilePath = "user_count.txt";

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
        if (ipAddress != null && !_uniqueUsers.ContainsKey(ipAddress))
        {
            _uniqueUsers[ipAddress] = true;

            File.WriteAllText(_userCountFilePath, $"Unique Users: {_uniqueUsers.Count}{Environment.NewLine}");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
