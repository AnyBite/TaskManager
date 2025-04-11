using System.Text.Json.Serialization;
using MediatR;
using TaskManager.Api.Requests;
using TaskManager.Application.Tasks; // Your command/handler namespaces
using TaskManager.Application.Tasks.Commands;
using TaskManager.Application.Tasks.Queries;
using TaskManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.PropertyNameCaseInsensitive = true; // optional but nice
});


// Register configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Register your services
builder.Services.AddInfrastructure(builder.Configuration);

// Register MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateTaskHandler).Assembly));

// Add Swagger (optional but helpful)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/tasks", async (CreateTaskCommand cmd, ISender sender) =>
{
    var id = await sender.Send(cmd);
    return Results.Created($"/tasks/{id}", id);
});

app.MapGet("/tasks/{id:guid}", async (Guid id, ISender sender) =>
{
    var task = await sender.Send(new GetTaskByIdQuery(id));
    return task is not null ? Results.Ok(task) : Results.NotFound();
});

app.MapPut("/tasks/{id:guid}/status", async (Guid id, UpdateTaskStatusRequest body, ISender sender) =>
{
    await sender.Send(new UpdateTaskStatusCommand(id, body.NewStatus));
    return Results.NoContent();
});

app.MapPut("/tasks/{id}/assign", async (Guid id, AssignTaskRequest req, ISender sender) =>
{
    await sender.Send(new AssignTaskCommand(id, req.UserId));
    return Results.NoContent();
});

app.Run();
