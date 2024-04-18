using api.Database;
using api.Entities;
using api.Record;
using Microsoft.EntityFrameworkCore;
namespace api.Routes;

// g:/C #/Learn/.net/api/Routes/Game.cs
public static class GameRoutes
{

    public static RouteGroupBuilder MapGameRoutes(this WebApplication app)
    {
        var group = app.MapGroup("games")
                        .WithParameterValidation();

        group.MapGet("/", (GameContext db) =>
        {
            var games = db.Games
                        .Include(g => g.Genre)
                        .Select(g => new {
                            Id = g.Id,
                            Name = g.Name,
                            GenreId = g.GenreId,
                            Genre = g.Genre!.Name,
                            Price = g.Price,
                            Date = g.Date
                        }).AsNoTracking();
            return Results.Json(new
            {
                Message = "Games found",
                Error = false,
                StatusCode = 200,
                Data = games
            });
        }).WithName("GetGames");
        group.MapGet("/{id}", (int id, GameContext db) =>
        {
            var game = db.Games.
                Include(g => g.Genre)
                .Select(g => new
                {
                    Id = g.Id,
                    Name = g.Name,
                    GenreId = g.GenreId,
                    Genre = g.Genre!.Name,
                    Price = g.Price,
                    Date = g.Date
                })
                .AsNoTracking()
                .FirstOrDefault(g => g.Id == id);
            if (game is null)
            {
                return Results.Json(new
                {
                    Message = "Game not found",
                    Error = true,
                    StatusCode = 404
                }, statusCode: 404);
            }
            return Results.Json(new
            {
                Message = "Game found",
                Error = false,
                StatusCode = 200,
                Data = game
            });
        }).WithName("GetGameById");
        group.MapPost("/", (CreateGame newGame, GameContext db) =>
        {
            GameEntity game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Genre = db.Genres.Find(newGame.GenreId),
                Price = newGame.Price,
                Date = newGame.Date
            };
            db.Games.Add(game);
            db.SaveChanges();
            var response = new
            {
                Id = game.Id,
                Name = game.Name,
                GenreId = game.GenreId,
                Genre = game.Genre!.Name,
                Price = game.Price,
                Date = game.Date
            };
            return Results.CreatedAtRoute("GetGameById", new { id = game.Id }, response);
        });
        group.MapPut("/{id}", (int id, UpdateGame updatedGame, GameContext db) =>
        {
            var game = db.Games.Find(id);
            if (game != null)
            {
                game.Name = updatedGame.Name;
                game.GenreId = updatedGame.GenreId;
                game.Genre = db.Genres.Find(updatedGame.GenreId);
                game.Price = updatedGame.Price;
                game.Date = updatedGame.Date;
                db.SaveChanges();
                return Results.Json(new
                {
                    Message = "Game updated",
                    Error = false,
                    StatusCode = 200,
                    Data = new
                    {
                        Id = game.Id,
                        Name = game.Name,
                        GenreId = game.GenreId,
                        Genre = game.Genre!.Name,
                        Price = game.Price,
                        Date = game.Date
                    }
                });
            }
            return Results.Json(new
            {
                Message = "Game not found",
                Error = true,
                StatusCode = 404
            }, statusCode: 404);
        }).WithName("UpdateGame");
        group.MapDelete("/{id}", (int id, GameContext db) =>
        {
            var game = db.Games.Where(g => g.Id == id);
            if (game is null)
            {
                return Results.Json(new
                {
                    Message = "Game not found",
                    Error = true,
                    StatusCode = 404
                }, statusCode: 404);
            }
            game.ExecuteDelete();
            db.SaveChanges();
            return Results.Json(new
            {
                Message = "Game deleted",
                Error = false,
                StatusCode = 200,
                Data = new
                {
                    Id = game.Id
                }
            });
        }).WithName("DeleteGame");
        return group;
    }

}
