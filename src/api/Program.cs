using api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var users = SeedData.SeedUsers(20);

app.MapGet("/users", () =>
{
    return users;
})
.WithName("GetUsers")
.WithOpenApi();

app.MapGet("/users/{id:int}", (int id) =>
{
    var user = users.FirstOrDefault(user => user.Id == id);
    if(user == null)
    {
        return Results.NotFound<RandomUser>(user);
    }

    return Results.Ok(user);
})
.WithName("GetUserById")
.WithOpenApi();

app.Run();