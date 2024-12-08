using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var activity = Activity.Current;
        if (activity != null)
        {
            activity.SetTag("custom.attribute", "User accessed Index page");
            activity.SetTag("user.id", "12345");
            activity.SetTag("request.ip", HttpContext.Connection.RemoteIpAddress?.ToString());
        }

        return View();
    }

    public IActionResult Privacy()
    {
        var activity = Activity.Current;
        if (activity != null)
        {
            activity.SetTag("custom.attribute", "User accessed Privacy page");
        }

        return View();
    }
}
