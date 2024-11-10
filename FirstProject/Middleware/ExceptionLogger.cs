using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

public class ExceptionLogger
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionLogger> _logger;

    public ExceptionLogger(RequestDelegate next, ILogger<ExceptionLogger> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request.");
            await LogErrorToFile(ex);
            throw;
        }
    }

    private Task LogErrorToFile(Exception ex)
    {
        var filePath = "logs.txt";
        var logMessage = $"{DateTime.UtcNow}: {ex.Message}\n{ex.StackTrace}\n\n";
        return File.AppendAllTextAsync(filePath, logMessage);
    }
}
