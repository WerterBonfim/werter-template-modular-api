namespace Werter.ModularApis.Shared.Configuration;

public static class ConfigurationExtensions
{
    public static T GetRequiredOptions<T>(this IConfiguration configuration, string sectionName)
        where T : class
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sectionName);

        var section = configuration.GetSection(sectionName);
        if (!section.Exists())
        {
            throw new InvalidOperationException(
                $"A seção de configuração '{sectionName}' não existe no appsettings e precisa ser definida.");
        }

        var options = section.Get<T>();
        if (options is null)
        {
            throw new InvalidOperationException(
                $"Não foi possível carregar as opções da seção '{sectionName}'. Verifique o formato no appsettings.");
        }

        return options;
    }
}
