namespace Werter.ModularApis.Shared.Configuration;

public sealed class ApiOptions
{
    public const string SectionName = "Api";

    /// <summary>
    /// Nome curto da API (ex.: API0xpto). Usado no OpenAPI, Scalar e OpenTelemetry.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Descrição da responsabilidade da API (ex.: API responsável por xpto).
    /// </summary>
    public string Description { get; init; } = string.Empty;

    public void EnsureValid()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new InvalidOperationException(
                $"A propriedade '{SectionName}:Name' não foi definida no appsettings e precisa ser preenchida.");
        }

        if (string.IsNullOrWhiteSpace(Description))
        {
            throw new InvalidOperationException(
                $"A propriedade '{SectionName}:Description' não foi definida no appsettings e precisa ser preenchida.");
        }
    }
}
