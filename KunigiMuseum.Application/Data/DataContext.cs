using KunigiMuseum.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KunigiMuseum.Application.Data;

public class DataContext : DbContext
{
    public DbSet<Team> Teams { get; set; }
    
    public DbSet<Game> Games { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>()
            .HasOne(x => x.Host)
            .WithMany(x => x.HostedGames)
            .HasForeignKey(x => x.HostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Game>()
            .HasOne(x => x.Winner)
            .WithMany(x => x.WonGames)
            .HasForeignKey(x => x.WinnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}