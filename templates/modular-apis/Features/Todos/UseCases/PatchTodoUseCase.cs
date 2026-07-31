using Werter.ModularApis.Features.Todos.Contracts;

namespace Werter.ModularApis.Features.Todos.UseCases;

public sealed class PatchTodoUseCase
{
    public TodoResponse Execute(int id, PatchTodoRequest request)
    {
        const string sampleName = "Sample todo";
        const bool sampleIsComplete = false;

        return new TodoResponse(
            id,
            request.Name ?? sampleName,
            request.IsComplete ?? sampleIsComplete
        );
    }
}
