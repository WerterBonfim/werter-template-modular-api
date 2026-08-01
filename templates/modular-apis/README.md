# Werter.ModularApis

Template de ASP.NET Core **Minimal APIs** (.NET 10) em **monolito modular**: um projeto Api em `src/`, com `Shared/` (cross-cutting) e `Modules/` (vertical slices por ação).

## Como executar

Na raiz da solution:

```bash
dotnet run --project src/Werter.ModularApis.Api --launch-profile http
```

A API sobe em **http://localhost:5080**.

### Nome e descrição da API

Personalize em `src/Werter.ModularApis.Api/appsettings.json` (seção `Api` — **obrigatória**):

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
  "OtlpEndpoint": "http://localhost:4317"
}
```

O `service.name` vem de `Api:Name`.  
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
Werter.ModularApis.sln
bruno/Todo/                         # Collection Bruno (OpenCollection)
src/
  Werter.ModularApis.Api/
    Program.cs                      # Host enxuto
    Shared/
      Configuration/                # ApiOptions, OpenTelemetryOptions
      Observability/                # OpenTelemetry (OTLP gRPC)
      OpenApi/                      # OpenAPI + Scalar
      Data/                         # Stub para persistência (sem EF ainda)
      Exceptions/                   # Handler global ProblemDetails
      Extensions/                   # AddSharedInfrastructure / UseSharedPipeline
    Modules/
      Todos/
        Todo.cs                     # Entidade do módulo
        TodosModule.cs              # DI + mapeamento de endpoints
        Features/
          ListTodos/                # Endpoint + UseCase + Response
          GetTodoById/
          CreateTodo/
          UpdateTodo/
          PatchTodo/
          DeleteTodo/
```

O módulo **Todos** é um exemplo stub (sem persistência) para servir de base ao criar novos módulos/slices.

Ao gerar o projeto, o nome pode ser trocado com `--FeatureName` (plural) e `--EntityName` (singular), por exemplo `--FeatureName Products --EntityName Product`.

## Porta HTTP

O perfil `http` usa a porta fixa **5080**, alinhada entre:

- `src/Werter.ModularApis.Api/Properties/launchSettings.json`
- `bruno/Todo/environments/local.yml`
