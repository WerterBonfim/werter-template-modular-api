using Werter.ModularApis.Api.Shared.Data;
using Werter.ModularApis.Api.Shared.Exceptions;
using Werter.ModularApis.Api.Shared.Observability;
using Werter.ModularApis.Api.Shared.OpenApi;

namespace Werter.ModularApis.Api.Shared.Extensions;

public static class SharedInfrastructureExtensions
{
    public static WebApplicationBuilder AddSharedInfrastructure(this WebApplicationBuilder builder)
    {
        builder.AddObservability();
        builder.AddApiDocumentation();
        builder.Services.AddHealthChecks();
        builder.Services.AddGlobalExceptionHandling();
        builder.Services.AddSharedData();

        return builder;
    }

    public static WebApplication UseSharedPipeline(this WebApplication app)
    {
        app.UseGlobalExceptionHandling();
        app.MapApiDocumentation();
        app.UseHttpsRedirection();

        return app;
    }
}
