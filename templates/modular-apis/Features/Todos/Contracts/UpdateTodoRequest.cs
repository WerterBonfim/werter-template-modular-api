namespace Werter.ModularApis.Features.Todos.Contracts;

public readonly record struct UpdateTodoRequest(
    string Name,
    bool IsComplete
);
