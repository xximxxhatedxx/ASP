using OpenTelemetry;
using OpenTelemetry.Exporter;
using System.Diagnostics;

public class CustomExporter : BaseExporter<Activity>
{
    public override ExportResult Export(in Batch<Activity> batch)
    {
        foreach (var activity in batch)
        {
            Console.WriteLine($"Custom Exporter: {activity.DisplayName}");
        }
        return ExportResult.Success;
    }
}
