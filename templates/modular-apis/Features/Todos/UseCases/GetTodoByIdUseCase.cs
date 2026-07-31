using Werter.ModularApis.Features.Todos.Contracts;

namespace Werter.ModularApis.Features.Todos.UseCases;

public sealed class GetTodoByIdUseCase
{
    public TodoResponse Execute(int id)
    {
        return new TodoResponse(id, "Sample todo", false);
    }
}
