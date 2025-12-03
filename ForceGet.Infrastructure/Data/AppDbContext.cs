using ForceGet.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace ForceGet.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Quote> Quotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<Quote>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Country).IsRequired().HasMaxLength(100);
            entity.Property(e => e.City).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Mode).IsRequired();
            entity.Property(e => e.MovementType).IsRequired();
            entity.Property(e => e.Incoterms).IsRequired();
            entity.Property(e => e.PackageType).IsRequired();
            entity.Property(e => e.Currency).IsRequired();
            entity.Property(e => e.OriginalAmount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ConvertedUSD).HasColumnType("decimal(18,2)");
        });

        base.OnModelCreating(modelBuilder);
    }
}
