namespace Werter.ModularApis.Api.Modules.Todos.Features.CreateTodo;

public readonly record struct CreateTodoRequest(
    string Name,
    bool IsComplete
);
