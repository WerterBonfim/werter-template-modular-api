using Werter.ModularApis.Features.Todos.Contracts;

namespace Werter.ModularApis.Features.Todos.UseCases;

public sealed class UpdateTodoUseCase
{
    public TodoResponse Execute(int id, UpdateTodoRequest request)
    {
        return new TodoResponse(id, request.Name, request.IsComplete);
    }
}
