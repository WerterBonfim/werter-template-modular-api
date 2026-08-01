namespace Werter.ModularApis.Api.Modules.Todos.Features.ListTodos;

public static class ListTodosEndpoint
{
    public static RouteGroupBuilder MapListTodos(this RouteGroupBuilder group)
    {
        group.MapGet("/", (ListTodosUseCase useCase) => Results.Ok(useCase.Execute()))
            .WithName("ListTodos");

        return group;
    }
}
