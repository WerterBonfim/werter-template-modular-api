namespace Werter.ModularApis.Modules.Todos.Features.PatchTodo;

public readonly record struct PatchTodoRequest(
    string? Name,
    bool? IsComplete
);
