namespace Werter.ModularApis.Features.Todos.Contracts;

public readonly record struct CreateTodoRequest(
    string Name,
    bool IsComplete
);
