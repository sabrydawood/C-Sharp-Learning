using api.Database;
using Microsoft.EntityFrameworkCore;

namespace api;

public static class Extintions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<GameContext>();
        db.Database.Migrate();
    }
}
