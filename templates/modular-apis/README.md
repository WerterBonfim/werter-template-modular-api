# Werter.ModularApis

Template de ASP.NET Core **Minimal APIs** (.NET 10) em **monolito modular** com **vertical slices**: um projeto em `src/`, código compartilhado em `Shared/`, capacidades de negócio em `Modules/` e espaço reservado em `tests/`.

## Como executar

Na raiz da solution:

```bash
dotnet run --project src/Werter.ModularApis --launch-profile http
```

A API sobe em **http://localhost:5080**.

### Nome e descrição da API

Personalize em `src/Werter.ModularApis/appsettings.json` (seção `Api` — **obrigatória**):

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

## Arquitetura (monolito modular + vertical slice)

O template organiza o código para crescer por **módulo de negócio** e, dentro de cada módulo, por **fatia vertical** (uma ação/caso de uso por pasta). O host (`Program.cs`) só compõe infraestrutura compartilhada e registra os módulos.

```text
Werter.ModularApis.sln
bruno/Todo/                         # Collections HTTP versionadas (Bruno)
tests/                              # Espaço para testes (unit/integration) — vazio de propósito
src/
  Werter.ModularApis/
    Program.cs                      # Host: DI, pipeline e Map dos módulos
    Shared/                         # Cross-cutting (não é regra de negócio de um módulo)
      Configuration/                # Options e leitura obrigatória de appsettings
      Observability/                # OpenTelemetry (traces, metrics, logs)
      OpenApi/                      # OpenAPI + Scalar
      Data/                         # Persistência compartilhada (stub; plugar EF/etc.)
      Exceptions/                   # Tratamento global de erros (ProblemDetails)
      Extensions/                   # Composição da infra Shared no host
      ValueObjects/                 # VOs reutilizáveis entre módulos (imutáveis)
    Modules/
      Todos/                        # Limite do módulo (bounded context do exemplo)
        Todo.cs                     # Entidade do módulo
        TodosModule.cs              # Registro de DI + mapeamento das rotas do módulo
        Features/                   # Vertical slices (uma pasta = uma ação)
          ListTodos/                # Endpoint + UseCase + Response
          GetTodoById/
          CreateTodo/
          UpdateTodo/
          PatchTodo/
          DeleteTodo/
```

### O que cada pasta representa

| Pasta | Papel na arquitetura |
|-------|----------------------|
| `src/` | Código de produção. Contém o projeto da API. |
| `tests/` | Reservada para projetos de teste. Adicione aqui unitários, integração, etc., quando precisar. |
| `bruno/` | Collections REST versionadas no Git para exercitar a API localmente. |
| `Program.cs` | Ponto de entrada do host: sobe infra compartilhada e mapeia módulos. Sem regra de negócio. |
| `Shared/` | Aspectos **transversais** usados por vários módulos (config, telemetria, erros, VOs comuns). Evite colocar regra específica de um domínio aqui. |
| `Shared/Configuration` | Contratos de configuração (`ApiOptions`, `OpenTelemetryOptions`) e helpers de leitura obrigatória. |
| `Shared/Observability` | Instrumentação OpenTelemetry (OTLP gRPC) pronta para Alloy/Collector. |
| `Shared/OpenApi` | Documentação OpenAPI e UI Scalar em Development. |
| `Shared/Data` | Extensão de persistência compartilhada (placeholder). É onde plugar DbContext/EF no futuro. |
| `Shared/Exceptions` | Handler global de exceções com resposta ProblemDetails. |
| `Shared/Extensions` | Orquestra `AddSharedInfrastructure` / `UseSharedPipeline` para o host ficar enxuto. |
| `Shared/ValueObjects` | Value Objects compartilhados (ex.: `Money`, `Email`). Preferir imutáveis (`readonly record struct`). |
| `Modules/` | Fronteira dos módulos de negócio do monolito. Cada pasta = um módulo. |
| `Modules/{Modulo}/` | Contém entidade(s) do módulo, `*Module.cs` (DI + endpoints) e `Features/`. |
| `Modules/{Modulo}/Features/{Acao}/` | **Vertical slice**: tudo daquela ação junto (`Endpoint`, `UseCase`, `Request`/`Response`). Sem MediatR. |

### Fluxo mental

1. Nova capacidade de negócio → novo item em `Modules/` (ou fatia nova em um módulo existente).
2. Código útil a **mais de um módulo** → `Shared/` (com cuidado para não virar “god folder”).
3. Conceito de valor reutilizável → `Shared/ValueObjects`.
4. Testes → projetos em `tests/` referenciando `src/`.

O módulo **Todos** é um exemplo stub (sem persistência) para servir de base ao criar novos módulos/slices.

Ao gerar o projeto, o nome pode ser trocado com `--FeatureName` (plural) e `--EntityName` (singular), por exemplo `--FeatureName Products --EntityName Product`.

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

## Porta HTTP

O perfil `http` usa a porta fixa **5080**, alinhada entre:

- `src/Werter.ModularApis/Properties/launchSettings.json`
- `bruno/Todo/environments/local.yml`
