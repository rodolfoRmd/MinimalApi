using MinimalApi.Data;
using MinimalApi.Services;
using MinimalAPI.ViewModels;
using MinmalApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDBContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
DatabaseManagementService.MigrationInitialisation(app);



app.MapGet("/v1/todos", (AppDBContext context) => {
    
        var todos = context.Todos.ToList();
         
    return todos is not null ? Results.Ok(todos) : Results.NotFound();
});
app.MapGet("/v1/todos/{id}", (AppDBContext context, string id) => {
    
        var todos = context.Todos.Where(x=>x.Id.ToString()==id);
         
    return todos is not null ? Results.Ok(todos) : Results.NotFound();
});

app.MapPost("/v1/todos", (AppDBContext context, CreateTodoViewModel model) =>
{
    var todo = model.MapTo();
    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    context.Todos.Add(todo);
    context.SaveChanges();

    return Results.Created($"/v1/todos/{todo.Id}", todo);
});


app.Run();
