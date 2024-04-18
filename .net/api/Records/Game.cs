using System.ComponentModel.DataAnnotations;
namespace api.Record;
public record class GameRecord(
    int Id,
    string Name,
    double Price,
    string Genre,
    DateOnly Date
);
public record class CreateGame(
    [Required][MinLength(3)][MaxLength(50)] string Name,
    [Required][Range(0, 10000)] double Price,
    [Required] int GenreId,
    DateOnly Date
);

public record class UpdateGame(
    [Required][MinLength(3)][MaxLength(50)] string Name,
    [Required][Range(0, 10000)] double Price,
    [Required] int GenreId,
    DateOnly Date
);