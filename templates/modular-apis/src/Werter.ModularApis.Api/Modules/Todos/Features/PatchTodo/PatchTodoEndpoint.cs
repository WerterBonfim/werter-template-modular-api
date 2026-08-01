namespace Werter.ModularApis.Api.Modules.Todos.Features.PatchTodo;

public static class PatchTodoEndpoint
{
    public static RouteGroupBuilder MapPatchTodo(this RouteGroupBuilder group)
    {
        group.MapPatch("/{id:int}", (int id, PatchTodoRequest request, PatchTodoUseCase useCase) =>
                Results.Ok(useCase.Execute(id, request)))
            .WithName("PatchTodo");

        return group;
    }
}
