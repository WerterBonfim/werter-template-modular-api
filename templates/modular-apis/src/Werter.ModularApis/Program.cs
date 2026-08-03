using Werter.ModularApis.Modules.Todos;
using Werter.ModularApis.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddSharedInfrastructure();
builder.Services.AddTodosModule();

var app = builder.Build();

app.UseSharedPipeline();
app.MapHealthChecks("/health");
app.MapTodosEndpoints();

app.Run();
