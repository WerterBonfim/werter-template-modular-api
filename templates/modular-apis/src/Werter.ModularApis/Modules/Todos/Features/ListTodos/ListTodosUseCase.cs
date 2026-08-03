namespace Werter.ModularApis.Modules.Todos.Features.ListTodos;

public sealed class ListTodosUseCase
{
    public ListTodosResponse[] Execute()
    {
        return
        [
            new ListTodosResponse(1, "Learn Minimal APIs", false),
            new ListTodosResponse(2, "Explore vertical slices", true)
        ];
    }
}
