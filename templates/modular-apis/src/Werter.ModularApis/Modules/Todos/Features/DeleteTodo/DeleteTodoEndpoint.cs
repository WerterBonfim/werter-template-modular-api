namespace Werter.ModularApis.Modules.Todos.Features.DeleteTodo;

public static class DeleteTodoEndpoint
{
    public static RouteGroupBuilder MapDeleteTodo(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:int}", (int id, DeleteTodoUseCase useCase) =>
            {
                useCase.Execute(id);
                return Results.NoContent();
            })
            .WithName("DeleteTodo");

        return group;
    }
}
