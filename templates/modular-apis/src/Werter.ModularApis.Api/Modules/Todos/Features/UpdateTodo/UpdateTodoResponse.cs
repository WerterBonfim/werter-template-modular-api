namespace Werter.ModularApis.Api.Modules.Todos.Features.UpdateTodo;

public readonly record struct UpdateTodoResponse(
    int Id,
    string Name,
    bool IsComplete
);
