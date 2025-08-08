using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StorageHandler.Features.Entrance;
using StorageHandler.Features.EntranceBucket;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Unit;

namespace StorageHandler.Utils.Data;

public class DatabaseContext : DbContext, IStorageContext
{
    public DatabaseContext(DbContextOptions options) : base(options) {}
    
    public DbSet<Unit> Units { get; set; } = null!;
    public DbSet<Resource> Resources { get; set; } = null!;
    public DbSet<Entrance> Entrances { get; set; } = null!;
    public DbSet<EntranceBucket> EntranceBuckets { get; set; } = null!;

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = new())
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Unit>(entity =>
        {
            entity.ToTable("units");
            entity.Property(unit => unit.Id)
                .IsRequired()
                .HasColumnName("id");
            entity.HasKey(unit => unit.Id)
                .HasName("id");
            entity.Property(unit => unit.Name)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("name");
            entity.HasIndex(unit => unit.Name)
                .IsUnique();
            entity.Property(unit => unit.ArchiveAt)
                .IsRequired(false)
                .HasColumnName("archive_at");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.ToTable("resources");
            entity.Property(resource => resource.Id)
                .IsRequired()
                .HasColumnName("id");
            entity.HasKey(unit => unit.Id)
                .HasName("id");
            entity.Property(unit => unit.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.HasIndex(unit => unit.Name)
                .IsUnique();
            entity.Property(unit => unit.ArchiveAt)
                .IsRequired(false)
                .HasColumnName("archive_at");
        });

        modelBuilder.Entity<Entrance>(entity =>
        {
            entity.ToTable("entrances");
            entity.Property(entrance => entrance.Id)
                .IsRequired()
                .HasColumnName("id");
            entity.HasKey(entrance => entrance.Id)
                .HasName("id");
            entity.Property(unit => unit.Number)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("number");
            entity.HasIndex(entrance => entrance.Number)
                .IsUnique();
            entity.Property(entrance => entrance.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");
            entity.HasMany(entrance => entrance.Buckets)
                .WithOne()
                .HasForeignKey(bucket => bucket.EntranceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<EntranceBucket>(entity =>
        {
            entity.ToTable("entrances_buckets");
            entity.Property(bucket => bucket.Id)
                .IsRequired()
                .HasColumnName("id");
            entity.HasKey(bucket => bucket.Id)
                .HasName("id");
            entity.Property(bucket => bucket.ResourceId)
                .HasColumnName("resource_id")
                .IsRequired();
            entity.Property(bucket => bucket.UnitId)
                .HasColumnName("unit_id")
                .IsRequired();
            entity.Property(bucket => bucket.EntranceId)
                .HasColumnName("entrance_id")
                .IsRequired();
            entity.Property(bucket => bucket.Quantity)
                .HasColumnName("quantity")
                .IsRequired();
            entity.HasOne(bucket => bucket.Resource)
                .WithMany()
                .HasForeignKey(bucket => bucket.ResourceId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(bucket => bucket.Unit)
                .WithMany()
                .HasForeignKey(bucket => bucket.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = new())
    {
        return Database.RollbackTransactionAsync(cancellationToken);
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken = new())
    {
        return Database.CommitTransactionAsync(cancellationToken);
    }
}