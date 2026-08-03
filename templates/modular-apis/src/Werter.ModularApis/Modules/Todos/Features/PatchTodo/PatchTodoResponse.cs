namespace Werter.ModularApis.Modules.Todos.Features.PatchTodo;

public readonly record struct PatchTodoResponse(
    int Id,
    string Name,
    bool IsComplete
);
