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
        // Fluent API configurations, if any
    }

    public DbSet<HockeyGoalie> HockeyGoalie{ get; set; } = null!;
    public DbSet<HockeyTeam> HockeyTeam{ get; set; } = null!;
    public DbSet<HockeyLeague> HockeyLeague{ get; set; } = null!;
    public DbSet<HockeyPosition> HockeyPosition{ get; set; } = null!;
    public DbSet<HockeyPlayer> HockeyPlayer{ get; set; } = null!;
    public DbSet<HockeyGoal> HockeyGoal{ get; set; } = null!;
    public DbSet<HockeyAssist> HockeyAssist{ get; set; } = null!;
}