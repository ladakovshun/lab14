using OpenTelemetry;
using OpenTelemetry.Trace;
using System;
using System.Diagnostics;

namespace WebApplication14.Exporters  // Простір імен для експортера
{
    public class CustomExporter : BaseExporter<Activity>
    {
        public override ExportResult Export(in Batch<Activity> batch)
        {
            foreach (var activity in batch)
            {
                Console.WriteLine($"Exporting activity: {activity.DisplayName}");
            }

            return ExportResult.Success;
        }
    }
}
