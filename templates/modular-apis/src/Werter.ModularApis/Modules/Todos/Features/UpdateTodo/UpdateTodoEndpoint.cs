namespace Werter.ModularApis.Modules.Todos.Features.UpdateTodo;

public static class UpdateTodoEndpoint
{
    public static RouteGroupBuilder MapUpdateTodo(this RouteGroupBuilder group)
    {
        group.MapPut("/{id:int}", (int id, UpdateTodoRequest request, UpdateTodoUseCase useCase) =>
                Results.Ok(useCase.Execute(id, request)))
            .WithName("UpdateTodo");

        return group;
    }
}
