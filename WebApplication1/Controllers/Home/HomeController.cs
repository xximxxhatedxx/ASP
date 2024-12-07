using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;

public class HomeController : Controller
{
    private readonly Tracer _tracer;

    public HomeController(TracerProvider tracerProvider)
    {
        _tracer = tracerProvider.GetTracer("example-tracer");
    }

    public IActionResult Index()
    {
        using (var span = _tracer.StartActiveSpan("IndexRequest"))
        {
            span.SetAttribute("http.method", "GET");
            span.SetAttribute("http.route", "/");
            span.SetAttribute("user.id", 123);
            span.AddEvent("Index page loaded");
        }
        return View();
    }
}
