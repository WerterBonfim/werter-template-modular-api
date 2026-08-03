namespace Werter.ModularApis.Modules.Todos.Features.CreateTodo;

public readonly record struct CreateTodoResponse(
    int Id,
    string Name,
    bool IsComplete
);
