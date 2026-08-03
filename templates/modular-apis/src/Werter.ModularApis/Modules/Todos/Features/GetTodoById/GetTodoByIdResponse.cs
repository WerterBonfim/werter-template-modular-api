namespace Werter.ModularApis.Modules.Todos.Features.GetTodoById;

public readonly record struct GetTodoByIdResponse(
    int Id,
    string Name,
    bool IsComplete
);
