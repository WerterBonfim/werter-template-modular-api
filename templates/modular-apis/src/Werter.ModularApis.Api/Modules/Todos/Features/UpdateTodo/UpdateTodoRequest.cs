namespace Werter.ModularApis.Api.Modules.Todos.Features.UpdateTodo;

public readonly record struct UpdateTodoRequest(
    string Name,
    bool IsComplete
);
