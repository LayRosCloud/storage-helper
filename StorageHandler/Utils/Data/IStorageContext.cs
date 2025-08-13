using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StorageHandler.Features.Entrance;
using StorageHandler.Features.EntranceBucket;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Unit;

namespace StorageHandler.Utils.Data;

public interface IStorageContext
{
    public DbSet<Unit> Units { get; }
    public DbSet<Resource> Resources { get; }
    public DbSet<Entrance> Entrances { get; }
    public DbSet<EntranceBucket> EntranceBuckets { get; }
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}