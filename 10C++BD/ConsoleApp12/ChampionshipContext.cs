using Microsoft.EntityFrameworkCore;

namespace SpanishChampionship;

public class ChampionshipContext : DbContext
{
    public ChampionshipContext(DbContextOptions<ChampionshipContext> options) : base(options) { }

    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Goal> Goals => Set<Goal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>().HasIndex(x => x.Name).IsUnique();

        modelBuilder.Entity<Match>()
            .HasOne(x => x.HomeTeam).WithMany(x => x.HomeMatches)
            .HasForeignKey(x => x.HomeTeamId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(x => x.AwayTeam).WithMany(x => x.AwayMatches)
            .HasForeignKey(x => x.AwayTeamId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Goal>()
            .HasOne(x => x.Player).WithMany()
            .HasForeignKey(x => x.PlayerId);

        modelBuilder.Entity<Goal>()
            .HasOne(x => x.Match).WithMany()
            .HasForeignKey(x => x.MatchId);
    }
}
