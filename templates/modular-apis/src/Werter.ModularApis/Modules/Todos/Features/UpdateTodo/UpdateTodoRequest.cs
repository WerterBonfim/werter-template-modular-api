namespace Werter.ModularApis.Modules.Todos.Features.UpdateTodo;

public readonly record struct UpdateTodoRequest(
    string Name,
    bool IsComplete
);
