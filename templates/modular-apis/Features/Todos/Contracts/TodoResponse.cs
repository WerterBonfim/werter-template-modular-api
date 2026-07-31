namespace Werter.ModularApis.Features.Todos.Contracts;

public readonly record struct TodoResponse(
    int Id,
    string Name,
    bool IsComplete
);
