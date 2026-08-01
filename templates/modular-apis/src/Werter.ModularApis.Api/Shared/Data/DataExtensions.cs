namespace Werter.ModularApis.Api.Shared.Data;

/// <summary>
/// Ponto de extensão para persistência compartilhada (ex.: EF Core DbContext).
/// Sem implementação de banco neste template — adicione o provider quando necessário.
/// </summary>
public static class DataExtensions
{
    public static IServiceCollection AddSharedData(this IServiceCollection services)
    {
        return services;
    }
}
