namespace Werter.ModularApis.Api.Shared.Exceptions;

public static class ExceptionHandlingExtensions
{
    public static IServiceCollection AddGlobalExceptionHandling(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

    public static WebApplication UseGlobalExceptionHandling(this WebApplication app)
    {
        app.UseExceptionHandler();

        return app;
    }
}
