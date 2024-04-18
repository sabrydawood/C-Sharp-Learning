using api.Database;
using api.Entities;
using api.Record;
using Microsoft.EntityFrameworkCore;
namespace api.Routes;

// g:/C #/Learn/.net/api/Routes/Game.cs
public static class GameRoutes
{
    private static readonly List<GameRecord> games = [
       new(
        1,
        "Games 1",
        11.55,
        "Genre 1",
        new DateOnly(2022, 3, 22)
    ),
    new (
        2,
        "GameRecord 2",
        12.55,
        "Genre 2",
        new DateOnly(2023, 1, 23)
    ),
    new (
        3,
        "GameRecord 3",
        13.55,
        "Genre 3",
        new DateOnly(2024, 4, 24)
        )
   ];

    public static RouteGroupBuilder MapGameRoutes(this WebApplication app)
    {
        var group = app.MapGroup("games")
                        .WithParameterValidation();
        group.MapGet("/", (GameContext db) =>
        {
            var games = db.Games
                        .Include(g => g.Genre)
                        .ToList();
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
            var game = db.Games.Find(id);
            if (game is null)
            {
                return Results.Json(new
                {
                    Message = "Game not found",
                    Error = true,
                    StatusCode = 404
                }, statusCode: 404);
            }
            var response = new
            {
                Id = game.Id,
                Name = game.Name,
                GenreId = game.GenreId,
                Genre = game.Genre!.Name,
                Price = game.Price,
                Date = game.Date
            };
            return Results.Json(new
            {
                Message = "Game found",
                Error = false,
                StatusCode = 200,
                Data = response
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
            var game = db.Games.Find(id);
            if (game is null)
            {
                return Results.Json(new
                {
                    Message = "Game not found",
                    Error = true,
                    StatusCode = 404
                }, statusCode: 404);
            }
            db.Games.Remove(game);
            db.SaveChanges();
            return Results.Json(new
            {
                Message = "Game deleted",
                Error = false,
                StatusCode = 200,
                Data = new {
                    Id = game.Id
                }
            });
        }).WithName("DeleteGame");
        return group;
    }

}
