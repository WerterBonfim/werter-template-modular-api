namespace Werter.ModularApis.Modules.Todos.Features.GetTodoById;

public sealed class GetTodoByIdUseCase
{
    public GetTodoByIdResponse Execute(int id)
    {
        return new GetTodoByIdResponse(id, "Sample todo", false);
    }
}
