using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using Werter.ModularApis.Shared.Configuration;

namespace Werter.ModularApis.Shared.OpenApi;

public static class ApiDocumentationExtensions
{
    public static WebApplicationBuilder AddApiDocumentation(this WebApplicationBuilder builder)
    {
        var apiOptions = builder.Configuration.GetRequiredOptions<ApiOptions>(ApiOptions.SectionName);
        apiOptions.EnsureValid();

        builder.Services.Configure<ApiOptions>(
            builder.Configuration.GetSection(ApiOptions.SectionName));

        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, _) =>
            {
                var configuredOptions = context.ApplicationServices
                    .GetRequiredService<IOptions<ApiOptions>>()
                    .Value;

                document.Info.Title = configuredOptions.Name;
                document.Info.Description = configuredOptions.Description;

                return Task.CompletedTask;
            });
        });

        return builder;
    }

    public static WebApplication MapApiDocumentation(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return app;
        }

        var apiOptions = app.Services.GetRequiredService<IOptions<ApiOptions>>().Value;

        app.MapOpenApi();
        app.MapScalarApiReference(options => options.WithTitle(apiOptions.Name));

        return app;
    }
}
