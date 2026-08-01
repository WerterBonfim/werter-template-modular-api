# Werter.ModularApis.Templates

Pacote de template `dotnet new` para criar APIs ASP.NET Core **Minimal APIs** (.NET 10) em **monolito modular** (`src/{Nome}.Api` + `Shared` + `Modules`).

| Item | Valor |
|------|--------|
| PackageId | `Werter.ModularApis.Templates` |
| Short name | `werter-modular-apis` |
| Framework | `net10.0` |

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)

## Instalar o template

### A partir deste repositório

```bash
dotnet pack Werter.ModularApis.Templates.csproj -c Release -o ./artifacts
dotnet new install ./artifacts/Werter.ModularApis.Templates.1.3.0.nupkg --force
```

### A partir da pasta do template (desenvolvimento)

Útil para iterar sem empacotar. Sempre use `--force` após mudanças na estrutura:

```bash
dotnet new install ./templates/modular-apis --force
```

## Criar um projeto

```bash
dotnet new werter-modular-apis -n MinhaApi
cd MinhaApi
dotnet run --project src/MinhaApi.Api --launch-profile http
```

A API sobe em **http://localhost:5080**.

### Módulo de exemplo dinâmico

Por padrão o módulo de exemplo se chama **Todos** / **Todo**. Você pode renomear na criação:

```bash
dotnet new werter-modular-apis -n CatalogoApi \
  --FeatureName Products \
  --EntityName Product
```

| Parâmetro | Default | Efeito |
|-----------|---------|--------|
| `--FeatureName` | `Todos` | Pasta `Modules/...`, `*Module`, rota plural (`/products`), tags |
| `--EntityName` | `Todo` | Entidade, classes das fatias, pasta Bruno, variável `{{product}}` |

Use **PascalCase** nos dois. Informe singular e plural explicitamente (ex.: `Pedidos` / `Pedido`).

### Atualizar o template instalado

Se a versão do pacote não mudar, o `dotnet new install` pode manter o cache antigo e **não refletir pastas novas**. Faça uninstall + install com `--force`:

```bash
dotnet new uninstall Werter.ModularApis.Templates
dotnet pack Werter.ModularApis.Templates.csproj -c Release -o ./artifacts
dotnet new install ./artifacts/Werter.ModularApis.Templates.1.3.0.nupkg --force
```

Confira a versão instalada com `dotnet new list werter`.

## O que o template gera

- Solution + projeto único em `src/{Nome}.Api`
- `Shared/` com Configuration, Observability, OpenApi, Data (stub), Exceptions e Extensions
- Módulo de exemplo em `Modules/{Feature}` com fatias por ação (Endpoint + UseCase + Request/Response)
- Health check em `/health`
- OpenAPI + Scalar UI em Development (`/openapi/v1.json`, `/scalar/v1`)
- Collection **Bruno** alinhada ao nome da entidade
- OpenTelemetry (traces, metrics e logs) via OTLP gRPC para Grafana Alloy
- Porta HTTP fixa **5080**
- README do projeto gerado com instruções de uso

### Endpoints de exemplo (default Todos)

| Método | Rota |
|--------|------|
| GET | `/health` |
| GET | `/todos` |
| GET | `/todos/{id}` |
| POST | `/todos` |
| PUT | `/todos/{id}` |
| PATCH | `/todos/{id}` |
| DELETE | `/todos/{id}` |

## Estrutura deste repositório

```text
template-aspnet/
├── Werter.ModularApis.Templates.csproj   # pacote NuGet (PackageType=Template)
├── templates/
│   └── modular-apis/                     # conteúdo gerado pelo template
│       ├── .template.config/
│       ├── Werter.ModularApis.sln
│       ├── bruno/Todo/
│       ├── src/Werter.ModularApis.Api/
│       │   ├── Shared/
│       │   └── Modules/Todos/
│       └── README.md
└── README.md                             # este arquivo
```

## OpenTelemetry

O template já vem instrumentado com OpenTelemetry (traces, metrics e logs), exportando via **OTLP gRPC** (padrão Alloy: `http://localhost:4317`).

Configuração em `appsettings.json`:

```json
"OpenTelemetry": {
  "OtlpEndpoint": "http://localhost:4317"
}
```

O `service.name` vem de `Api:Name`.  
No Compose/Kubernetes, você também pode usar `OTEL_EXPORTER_OTLP_ENDPOINT` / `OTEL_EXPORTER_OTLP_PROTOCOL=grpc`.  
`/health` permanece disponível para orquestração e **não gera spans** (também exclui `/openapi` e `/scalar` do tracing).

## Desenvolver o conteúdo do template

Para editar e testar a API diretamente (sem empacotar):

```bash
dotnet run --project templates/modular-apis/src/Werter.ModularApis.Api --launch-profile http
```

Documentação detalhada do projeto gerado: [templates/modular-apis/README.md](templates/modular-apis/README.md).

## Bruno

O **Bruno** é o cliente REST HTTP do template. O objetivo é **versionar as collections no Git** e compartilhá-las entre as equipes de desenvolvimento: cada request fica em arquivo no repositório, todos usam as mesmas collections, ganham produtividade no dia a dia e mantêm uma documentação viva dos endpoints.

É necessário instalar o Bruno em uma destas formas:

1. **Extensão no VS Code** — Marketplace: [bruno-api-client.bruno](https://marketplace.visualstudio.com/items?itemName=bruno-api-client.bruno)
2. **Extensão no Cursor** — busque por **Bruno** no painel de Extensions (mesmo pacote da VS Code)
3. **Bruno Desktop** — [usebruno.com/downloads](https://www.usebruno.com/downloads)

### Usar a collection

A collection fica em `bruno/Todo/` no projeto gerado.

1. Abra a pasta `bruno/Todo/` no Bruno (onde está o `opencollection.yml`)
2. Selecione o environment **local**
3. Com a API rodando, execute os requests

## Licença

Uso interno / conforme definido pelo autor do repositório.
