namespace api.Entities;

/*
To make One To One Relationship with Genre
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
*/

public class GameEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
    public double Price { get; set; }
    public DateOnly Date { get; set; }
}
