namespace Werter.ModularApis.Api.Modules.Todos.Features.PatchTodo;

public sealed class PatchTodoUseCase
{
    public PatchTodoResponse Execute(int id, PatchTodoRequest request)
    {
        const string sampleName = "Sample todo";
        const bool sampleIsComplete = false;

        return new PatchTodoResponse(
            id,
            request.Name ?? sampleName,
            request.IsComplete ?? sampleIsComplete
        );
    }
}
