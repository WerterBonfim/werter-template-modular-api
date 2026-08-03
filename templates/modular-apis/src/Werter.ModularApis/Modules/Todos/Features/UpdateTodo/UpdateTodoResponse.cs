namespace Werter.ModularApis.Modules.Todos.Features.UpdateTodo;

public readonly record struct UpdateTodoResponse(
    int Id,
    string Name,
    bool IsComplete
);
