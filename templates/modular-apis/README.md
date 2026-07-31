# Werter.ModularApis

Template de ASP.NET Core **Minimal APIs** (.NET 10) organizado em **vertical slices**, com health check, OpenAPI, Scalar e collection Bruno pronta para uso.

## Como executar

```bash
dotnet run --launch-profile http
```

A API sobe em **http://localhost:5080**.

### Endpoints úteis

| Recurso | URL |
|---------|-----|
| Health | http://localhost:5080/health |
| Todos | http://localhost:5080/todos |
| OpenAPI | http://localhost:5080/openapi/v1.json |
| Scalar (Development) | http://localhost:5080/scalar/v1 |

## Bruno (cliente REST)

A collection padrão fica na pasta **`bruno/Todo/`** (OpenCollection YAML, com `opencollection.yml`).

### Instalar o Bruno

Escolha uma opção:

1. **Extensão no VS Code ou Cursor** — busque por **Bruno** no painel de Extensions, ou instale pelo Marketplace: [bruno-api-client.bruno](https://marketplace.visualstudio.com/items?itemName=bruno-api-client.bruno)
2. **App desktop** — baixe em [usebruno.com/downloads](https://www.usebruno.com/downloads)

### Abrir a collection

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
bruno/Todo/                 # Collection Bruno (OpenCollection)
Program.cs                  # Pipeline, health, OpenAPI/Scalar
```

A feature **Todos** é um exemplo stub (sem persistência) para servir de base ao criar novos slices.

## Porta HTTP

O perfil `http` usa a porta fixa **5080**, alinhada entre:

- `Properties/launchSettings.json`
- `bruno/Todo/environments/local.yml`
- `Werter.ModularApis.http` (REST Client do editor)
