namespace Werter.ModularApis.Modules.Todos.Features.CreateTodo;

public readonly record struct CreateTodoRequest(
    string Name,
    bool IsComplete
);
