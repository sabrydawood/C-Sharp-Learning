using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Database;

public class GameContext(DbContextOptions<GameContext> options) :
    DbContext(options)
{
    public DbSet<GameEntity> Games => Set<GameEntity>();
    public DbSet<Genre> Genres => Set<Genre>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
           new { Id = 1, Name = "Action" },
           new { Id = 2, Name = "RPG" },
           new { Id = 3, Name = "Shooter" },
           new { Id = 4, Name = "Strategy" },
           new { Id = 5, Name = "Puzzle" }
        );
        // modelBuilder.Entity<GameContext>().Property(g =>)
        
    }
}
