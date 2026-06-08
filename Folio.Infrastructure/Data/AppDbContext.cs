using Folio.Core.Common;
using Folio.Core.Entities;
using Folio.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Folio.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<FileRecord> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileRecord>(entity =>
        {
            entity.Property(e => e.OriginalFileName)
                  .IsRequired()
                  .HasMaxLength(500);
            entity.Property(e => e.BlobPath)
                  .IsRequired();
            entity.Property(e => e.Status)
                  .HasConversion<string>();
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseAuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Modified)
                entry.Entity.UpdatedAt = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}