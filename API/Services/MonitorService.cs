using System.Diagnostics;
using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.Span;
using ILogger = Serilog.ILogger;

namespace API.Services;

public class MonitorService
{
    public static ILogger Log => Serilog.Log.Logger; 
    public static readonly string ServiceName = Assembly.GetEntryAssembly().GetName().Name ?? "unknown";
    public static TracerProvider TracerProvider;
    public static ActivitySource ActivitySource = new ActivitySource(ServiceName);

    static MonitorService()
    {
        // Serilog
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341")
            .Enrich.WithSpan()
            .CreateLogger();
        
        Serilog.Log.Information("Starting Logging");
        
        // OpenTelemetry
        TracerProvider = Sdk.CreateTracerProviderBuilder()
            .AddZipkinExporter(options =>
            {
                options.Endpoint = new Uri("http://zipkin:9411/api/v2/spans");
            })
            .AddSource(ActivitySource.Name)
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName))
            .Build();
    }
}