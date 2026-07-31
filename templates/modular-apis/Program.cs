using Werter.ModularApis.Features.Todos;
using Werter.ModularApis.Observability;
using Werter.ModularApis.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddObservability();
builder.AddApiDocumentation();
builder.Services.AddHealthChecks();
builder.Services.AddTodosFeature();

var app = builder.Build();

app.MapApiDocumentation();
app.UseHttpsRedirection();
app.MapHealthChecks("/health");
app.MapTodosEndpoints();

app.Run();
