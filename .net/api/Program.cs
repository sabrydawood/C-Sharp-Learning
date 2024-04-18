using api;
using api.Database;
using api.Entities;
using api.Routes;
var builder = WebApplication.CreateBuilder(args);
var connString = "Data Source=Games.db";
builder.Services.AddSqlite<GameContext>(connString);
var app = builder.Build();

app.UseHttpsRedirection();
app.MapGameRoutes();
app.MigrateDb();
app.Run();

