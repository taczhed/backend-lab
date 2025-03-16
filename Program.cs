using backend_lab.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Mock data
List<User> users = new List<User>{ new User("1", "test@test.pl", "Test123!") };
User GetUserById(string id) => users.SingleOrDefault((p) => p.Id == id);
void AddUser() => users.Add(new User((users.Count() + 1).ToString(), "Test@test.pl", "Test123!"));

app.MapGet("/{id}", (string id) => GetUserById(id));

app.MapPost("/", () => {
    AddUser();
    return Results.Created(); 
});

app.MapPut("/", () => { return Results.Created(); });
app.MapPatch("/", () => { return Results.Ok(); });
app.MapDelete("/", () => { return Results.NoContent(); });

app.Run("https://localhost:4200");
//Swagger: https://localhost:4200/swagger/index.html