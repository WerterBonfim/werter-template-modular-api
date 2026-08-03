namespace Werter.ModularApis.Shared.Configuration;

public sealed class OpenTelemetryOptions
{
    public const string SectionName = "OpenTelemetry";

    public string OtlpEndpoint { get; init; } = string.Empty;

    public void EnsureValid()
    {
        if (string.IsNullOrWhiteSpace(OtlpEndpoint))
        {
            throw new InvalidOperationException(
                $"A propriedade '{SectionName}:OtlpEndpoint' não foi definida no appsettings e precisa ser preenchida.");
        }

        if (!Uri.TryCreate(OtlpEndpoint, UriKind.Absolute, out _))
        {
            throw new InvalidOperationException(
                $"A propriedade '{SectionName}:OtlpEndpoint' deve ser uma URI absoluta válida. Valor atual: '{OtlpEndpoint}'.");
        }
    }
}
