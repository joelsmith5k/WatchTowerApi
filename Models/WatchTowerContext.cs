using Microsoft.EntityFrameworkCore;

namespace WatchTowerApi.Models;

public class WatchTowerContext : DbContext
{
    public WatchTowerContext(DbContextOptions<WatchTowerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Setup foreign key references
        modelBuilder.Entity<HockeyGoalie>()
            .HasOne(b => b.HockeyLeague)
            .WithMany(a => a.HockeyGoalies)
            .HasForeignKey(b => b.LeagueID);

        modelBuilder.Entity<HockeyGoalie>()
            .HasOne(b => b.HockeyTeam)
            .WithMany(a => a.HockeyGoalies)
            .HasForeignKey(b => b.CurrentTeamID);
    }

    public DbSet<HockeyGoalie> HockeyGoalie{ get; set; } = null!;
    public DbSet<HockeyTeam> HockeyTeam{ get; set; } = null!;
    public DbSet<HockeyLeague> HockeyLeague{ get; set; } = null!;
    public DbSet<HockeyPosition> HockeyPosition{ get; set; } = null!;
    public DbSet<HockeyPlayer> HockeyPlayer{ get; set; } = null!;
    public DbSet<HockeyGoal> HockeyGoal{ get; set; } = null!;
    public DbSet<HockeyAssist> HockeyAssist{ get; set; } = null!;
}