using Microsoft.AspNetCore.Mvc;
using Serilog;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        Log.Information("Application started at {Time}", DateTime.Now);
        Log.Debug("Debug message");
        Log.Warning("This is a warning message");
        Log.Error("An error occurred at {Time}", DateTime.Now);
        Log.Fatal("Fatal error: {ErrorDetails}", "OutOfMemory");

        return View();
    }

    public IActionResult Error()
    {
        try
        {
            throw new InvalidOperationException("An unexpected error occurred.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred in the Error action at {Time}", DateTime.Now);
        }

        return View();
    }
}
