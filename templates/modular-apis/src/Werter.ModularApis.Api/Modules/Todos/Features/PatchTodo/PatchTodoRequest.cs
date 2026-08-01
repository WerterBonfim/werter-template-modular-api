namespace Werter.ModularApis.Api.Modules.Todos.Features.PatchTodo;

public readonly record struct PatchTodoRequest(
    string? Name,
    bool? IsComplete
);
