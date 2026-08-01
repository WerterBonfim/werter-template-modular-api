namespace Werter.ModularApis.Api.Modules.Todos.Features.CreateTodo;

public static class CreateTodoEndpoint
{
    public static RouteGroupBuilder MapCreateTodo(this RouteGroupBuilder group)
    {
        group.MapPost("/", (CreateTodoRequest request, CreateTodoUseCase useCase) =>
            {
                var todo = useCase.Execute(request);
                return Results.Created($"/todos/{todo.Id}", todo);
            })
            .WithName("CreateTodo");

        return group;
    }
}
