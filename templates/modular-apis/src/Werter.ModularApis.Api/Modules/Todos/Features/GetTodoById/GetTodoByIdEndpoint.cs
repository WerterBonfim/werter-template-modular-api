namespace Werter.ModularApis.Api.Modules.Todos.Features.GetTodoById;

public static class GetTodoByIdEndpoint
{
    public static RouteGroupBuilder MapGetTodoById(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:int}", (int id, GetTodoByIdUseCase useCase) => Results.Ok(useCase.Execute(id)))
            .WithName("GetTodoById");

        return group;
    }
}
