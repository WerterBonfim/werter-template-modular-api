using Werter.ModularApis.Modules.Todos.Features.CreateTodo;
using Werter.ModularApis.Modules.Todos.Features.DeleteTodo;
using Werter.ModularApis.Modules.Todos.Features.GetTodoById;
using Werter.ModularApis.Modules.Todos.Features.ListTodos;
using Werter.ModularApis.Modules.Todos.Features.PatchTodo;
using Werter.ModularApis.Modules.Todos.Features.UpdateTodo;

namespace Werter.ModularApis.Modules.Todos;

public static class TodosModule
{
    public static IServiceCollection AddTodosModule(this IServiceCollection services)
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

        group.MapListTodos();
        group.MapGetTodoById();
        group.MapCreateTodo();
        group.MapUpdateTodo();
        group.MapPatchTodo();
        group.MapDeleteTodo();

        return endpoints;
    }
}
