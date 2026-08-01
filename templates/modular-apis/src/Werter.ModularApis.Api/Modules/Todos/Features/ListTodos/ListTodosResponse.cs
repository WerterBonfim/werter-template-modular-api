namespace Werter.ModularApis.Api.Modules.Todos.Features.ListTodos;

public readonly record struct ListTodosResponse(
    int Id,
    string Name,
    bool IsComplete
);
