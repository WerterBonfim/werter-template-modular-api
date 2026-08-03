namespace Werter.ModularApis.Modules.Todos;

public sealed class Todo
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsComplete { get; set; }
}
