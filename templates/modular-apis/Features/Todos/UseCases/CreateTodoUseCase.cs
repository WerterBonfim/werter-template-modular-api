using Werter.ModularApis.Features.Todos.Contracts;

namespace Werter.ModularApis.Features.Todos.UseCases;

public sealed class CreateTodoUseCase
{
    public TodoResponse Execute(CreateTodoRequest request)
    {
        return new TodoResponse(1, request.Name, request.IsComplete);
    }
}
