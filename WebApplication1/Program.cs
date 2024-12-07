using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using System.Diagnostics;
using OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .AddSource(DiagnosticsConfig.ServiceName)
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(DiagnosticsConfig.ServiceName, serviceVersion: "1.0.0")
                    .AddAttributes(new Dictionary<string, object>
                    {
                        ["environment"] = "production",
                        ["service.instance.id"] = Environment.MachineName,
                    }))
            .AddAspNetCoreInstrumentation(options =>
            {
                options.Filter = (req) =>
                {
                    return !req.Request.Path.StartsWithSegments("/health")
                           && !req.Request.Path.StartsWithSegments("/metrics");
                };

                options.EnrichWithHttpRequest = (activity, request) =>
                {
                    activity.SetTag("custom.request.header",
                        request.Headers["custom-header"]);
                };
            })
            .AddOtlpExporter(opts =>
            {
                opts.Endpoint = new Uri("http://localhost:4317");
            })
            .AddConsoleExporter();
    })
    .WithMetrics(metricsProviderBuilder =>
    {
        metricsProviderBuilder
            .AddMeter(DiagnosticsConfig.ServiceName)
            .AddAspNetCoreInstrumentation()
            .AddOtlpExporter(opts =>
            {
                opts.Endpoint = new Uri("http://localhost:4317");
            });
    });

var app = builder.Build();                 

app.MapGet("/", async (ILogger<Program> logger) =>
{
    using var activity = DiagnosticsConfig.ActivitySource.StartActivity("HomeRequest");
    activity?.SetTag("custom.tag", "example");

    logger.LogInformation("Processing home request");
    await Task.Delay(Random.Shared.Next(100, 1000));
    return "Hello from OpenTelemetry enabled service!";
});

app.Run();

public static class DiagnosticsConfig
{
    public const string ServiceName = "MyService";
    public static readonly ActivitySource ActivitySource = new(ServiceName);
}