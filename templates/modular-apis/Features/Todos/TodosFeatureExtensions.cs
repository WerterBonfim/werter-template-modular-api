using Werter.ModularApis.Features.Todos.Contracts;
using Werter.ModularApis.Features.Todos.UseCases;

namespace Werter.ModularApis.Features.Todos;

public static class TodosFeatureExtensions
{
    public static IServiceCollection AddTodosFeature(this IServiceCollection services)
    {
        services.AddSingleton<ListTodosUseCase>();
        services.AddSingleton<GetTodoByIdUseCase>();
        services.AddSingleton<CreateTodoUseCase>();
        services.AddSingleton<UpdateTodoUseCase>();
        services.AddSingleton<PatchTodoUseCase>();
        services.AddSingleton<DeleteTodoUseCase>();

        return services;
    }

    public static IEndpointRouteBuilder MapTodosEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/todos").WithTags("Todos");

        group.MapGet("/", (ListTodosUseCase useCase) => Results.Ok(useCase.Execute()))
            .WithName("ListTodos");

        group.MapGet("/{id:int}", (int id, GetTodoByIdUseCase useCase) => Results.Ok(useCase.Execute(id)))
            .WithName("GetTodoById");

        group.MapPost("/", (CreateTodoRequest request, CreateTodoUseCase useCase) =>
            {
                var todo = useCase.Execute(request);
                return Results.Created($"/todos/{todo.Id}", todo);
            })
            .WithName("CreateTodo");

        group.MapPut("/{id:int}", (int id, UpdateTodoRequest request, UpdateTodoUseCase useCase) =>
                Results.Ok(useCase.Execute(id, request)))
            .WithName("UpdateTodo");

        group.MapPatch("/{id:int}", (int id, PatchTodoRequest request, PatchTodoUseCase useCase) =>
                Results.Ok(useCase.Execute(id, request)))
            .WithName("PatchTodo");

        group.MapDelete("/{id:int}", (int id, DeleteTodoUseCase useCase) =>
            {
                useCase.Execute(id);
                return Results.NoContent();
            })
            .WithName("DeleteTodo");

        return endpoints;
    }
}
