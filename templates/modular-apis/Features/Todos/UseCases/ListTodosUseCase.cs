using Werter.ModularApis.Features.Todos.Contracts;

namespace Werter.ModularApis.Features.Todos.UseCases;

public sealed class ListTodosUseCase
{
    public TodoResponse[] Execute()
    {
        return
        [
            new TodoResponse(1, "Learn Minimal APIs", false),
            new TodoResponse(2, "Explore vertical slices", true)
        ];
    }
}
