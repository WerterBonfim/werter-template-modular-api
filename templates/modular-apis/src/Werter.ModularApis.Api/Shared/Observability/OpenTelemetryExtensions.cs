using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Werter.ModularApis.Api.Shared.Configuration;

namespace Werter.ModularApis.Api.Shared.Observability;

public static class OpenTelemetryExtensions
{
    public static WebApplicationBuilder AddObservability(this WebApplicationBuilder builder)
    {
        var apiOptions = builder.Configuration.GetRequiredOptions<ApiOptions>(ApiOptions.SectionName);
        apiOptions.EnsureValid();

        var openTelemetryOptions = builder.Configuration
            .GetRequiredOptions<OpenTelemetryOptions>(OpenTelemetryOptions.SectionName);
        openTelemetryOptions.EnsureValid();

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(
                serviceName: apiOptions.Name,
                serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString()))
            .UseOtlpExporter(OtlpExportProtocol.Grpc, new Uri(openTelemetryOptions.OtlpEndpoint))
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation(options =>
                {
                    options.Filter = context =>
                        !context.Request.Path.StartsWithSegments("/health")
                        && !context.Request.Path.StartsWithSegments("/openapi")
                        && !context.Request.Path.StartsWithSegments("/scalar");
                })
                .AddHttpClientInstrumentation())
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation())
            .WithLogging();

        return builder;
    }
}
