namespace Werter.ModularApis.Api.Modules.Todos.Features.PatchTodo;

public readonly record struct PatchTodoResponse(
    int Id,
    string Name,
    bool IsComplete
);
