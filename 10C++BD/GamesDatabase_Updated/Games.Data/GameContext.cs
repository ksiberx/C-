using Games.Models;
using Microsoft.EntityFrameworkCore;

namespace Games.Data;

public class GameContext : DbContext
{
    public DbSet<Game> Games => Set<Game>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=GamesDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
