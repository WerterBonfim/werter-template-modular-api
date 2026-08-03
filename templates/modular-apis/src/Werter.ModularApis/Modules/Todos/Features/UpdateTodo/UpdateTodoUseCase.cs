namespace Werter.ModularApis.Modules.Todos.Features.UpdateTodo;

public sealed class UpdateTodoUseCase
{
    public UpdateTodoResponse Execute(int id, UpdateTodoRequest request)
    {
        return new UpdateTodoResponse(id, request.Name, request.IsComplete);
    }
}
