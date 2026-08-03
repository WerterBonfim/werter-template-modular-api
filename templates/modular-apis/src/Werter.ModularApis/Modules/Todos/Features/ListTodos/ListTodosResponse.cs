namespace Werter.ModularApis.Modules.Todos.Features.ListTodos;

public readonly record struct ListTodosResponse(
    int Id,
    string Name,
    bool IsComplete
);
