namespace Werter.ModularApis.Features.Todos.Contracts;

public readonly record struct PatchTodoRequest(
    string? Name,
    bool? IsComplete
);
