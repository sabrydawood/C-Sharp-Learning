using api;
using api.Database;
using api.Entities;
using api.Routes;
var builder = WebApplication.CreateBuilder(args);
var connString = "Data Source=Games.db";
builder.Services.AddSqlite<GameContext>(connString);
var app = builder.Build();

app.UseHttpsRedirection();
app.MapGet("/", () => {
    return Results.Json(new
    {
        Message = "Server is Up!",
        Error = false,
        StatusCode = 200,
        Date = DateTime.Now.ToString()
    });
});
app.MapGameRoutes();
app.MigrateDb();
app.Run();

