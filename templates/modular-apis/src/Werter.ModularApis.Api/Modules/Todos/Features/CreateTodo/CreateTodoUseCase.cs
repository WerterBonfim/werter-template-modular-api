namespace Werter.ModularApis.Api.Modules.Todos.Features.CreateTodo;

public sealed class CreateTodoUseCase
{
    public CreateTodoResponse Execute(CreateTodoRequest request)
    {
        return new CreateTodoResponse(1, request.Name, request.IsComplete);
    }
}
