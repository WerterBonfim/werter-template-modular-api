namespace Werter.ModularApis.Api.Modules.Todos.Features.GetTodoById;

public sealed class GetTodoByIdUseCase
{
    public GetTodoByIdResponse Execute(int id)
    {
        return new GetTodoByIdResponse(id, "Sample todo", false);
    }
}
