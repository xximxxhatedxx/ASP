using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Threading.Tasks;

public class LogActionFilter : IActionFilter
{
    private readonly string _logFilePath = "action_log.txt";

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var methodName = context.ActionDescriptor.DisplayName;
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        File.AppendAllText(_logFilePath, $"Method: {methodName}, Time: {timestamp}{Environment.NewLine}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
