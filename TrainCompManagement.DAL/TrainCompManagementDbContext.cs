using Microsoft.EntityFrameworkCore;
using TrainCompManagement.DAL.Entities;

namespace TrainCompManagement.DAL;

public class TrainCompManagementDbContext : DbContext
{
    public DbSet<TrainInformation?> TrainInformation { get; set; }
    public DbSet<TrainTreePath> TrainTreePath { get; set; }

    public TrainCompManagementDbContext(DbContextOptions<TrainCompManagementDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<TrainInformation>()
            .HasIndex(u =>new {u.UniqueNumber})
            .IsUnique(true);
        
        builder.Entity<TrainInformation>()
            .HasMany<TrainTreePath>(m =>m.Parent)
            .WithOne(o=>o.Ancestor);
        
        builder.Entity<TrainInformation>()
            .HasMany<TrainTreePath>(m =>m.Children)
            .WithOne(o=>o.Descendant);
    }
}