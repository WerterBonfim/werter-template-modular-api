# Werter.ModularApis

Template de ASP.NET Core **Minimal APIs** (.NET 10) organizado em **vertical slices**, com health check, OpenAPI, Scalar, OpenTelemetry e collection Bruno pronta para uso.

## Como executar

```bash
dotnet run --launch-profile http
```

A API sobe em **http://localhost:5080**.

### Nome e descrição da API

Personalize em `appsettings.json` (seção `Api` — **obrigatória**):

```json
"Api": {
  "Name": "API0xpto",
  "Description": "API responsável por xpto"
}
```

Esses valores aparecem no OpenAPI/Scalar e no `service.name` do OpenTelemetry.  
Se a seção `Api` ou `OpenTelemetry` não existir (ou propriedades obrigatórias estiverem vazias), a aplicação falha na inicialização com mensagem explícita.

### Endpoints úteis

| Recurso | URL |
|---------|-----|
| Health | http://localhost:5080/health |
| Todos | http://localhost:5080/todos |
| OpenAPI | http://localhost:5080/openapi/v1.json |
| Scalar (Development) | http://localhost:5080/scalar/v1 |

## OpenTelemetry

A API exporta **traces**, **metrics** e **logs** via **OTLP gRPC**, pronta para o Grafana Alloy (ou OpenTelemetry Collector) coletar.

Configuração em `appsettings.json`:

```json
"OpenTelemetry": {
  "ServiceName": "Werter.ModularApis",
  "OtlpEndpoint": "http://localhost:4317"
}
```

No Compose/Kubernetes, use `OTEL_EXPORTER_OTLP_ENDPOINT` (e `OTEL_EXPORTER_OTLP_PROTOCOL=grpc` se necessário).

Instrumentação estável incluída: ASP.NET Core, HttpClient e Runtime.  
`/health` fica disponível para containers/orquestração e **não gera spans**; `/openapi` e `/scalar` também são excluídos do tracing.

## Bruno (cliente REST)

O **Bruno** é o cliente REST HTTP do projeto. O objetivo é **versionar as collections no Git** e compartilhá-las entre as equipes de desenvolvimento: os arquivos de request ficam no repositório, todos os devs usam as mesmas collections, aumentam a produtividade e contam com uma documentação prática dos endpoints.

É necessário instalar o Bruno em uma destas formas:

1. **Extensão no VS Code** — Marketplace: [bruno-api-client.bruno](https://marketplace.visualstudio.com/items?itemName=bruno-api-client.bruno)
2. **Extensão no Cursor** — busque por **Bruno** no painel de Extensions (mesmo pacote da VS Code)
3. **Bruno Desktop** — [usebruno.com/downloads](https://www.usebruno.com/downloads)

### Abrir a collection

A collection padrão fica na pasta **`bruno/Todo/`** (OpenCollection YAML, com `opencollection.yml`).

1. No Bruno, abra a pasta `bruno/Todo/` (é onde está o `opencollection.yml`)
2. Selecione o environment **local** (`todo` = `http://localhost:5080`)
3. Com a API rodando, execute os requests da collection

### O que já vem na collection

- `GET /health`
- `GET /todos`
- `GET /todos/{id}`
- `POST /todos`
- `PUT /todos/{id}`
- `PATCH /todos/{id}`
- `DELETE /todos/{id}`

## Estrutura do código

```text
Features/
  Todos/
    Contracts/              # Request/Response (DTOs)
    UseCases/               # Casos de uso concretos (sem MediatR)
    TodosFeatureExtensions.cs
Observability/              # OpenTelemetry (OTLP gRPC)
OpenApi/                    # Documentação OpenAPI/Scalar (Api:Name/Description)
Configuration/              # ApiOptions e configs da API
bruno/Todo/                 # Collection Bruno (OpenCollection)
Program.cs                  # Pipeline enxuto
```

A feature **Todos** é um exemplo stub (sem persistência) para servir de base ao criar novos slices.

## Porta HTTP

O perfil `http` usa a porta fixa **5080**, alinhada entre:

- `Properties/launchSettings.json`
- `bruno/Todo/environments/local.yml`
- `Werter.ModularApis.http` (REST Client do editor)
