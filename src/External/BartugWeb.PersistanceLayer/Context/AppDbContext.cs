using System.Reflection.Metadata;
using BartugWeb.DomainLayer.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace BartugWeb.PersistanceLayer.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries<Entity>();
        foreach (var item in entries)
        {
            if (item.State == EntityState.Added)
                item.Property(p => p.CreatedAt).CurrentValue = DateTime.UtcNow;
            
            if (item.State == EntityState.Modified)
                item.Property(p => p.UpdatedAt).CurrentValue = DateTime.UtcNow;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}