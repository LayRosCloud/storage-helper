using Microsoft.EntityFrameworkCore;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.EntranceBucket;

public class EntranceBucketRepository : IEntranceBucketRepository
{
    private readonly IStorageContext _storage;

    public EntranceBucketRepository(IStorageContext storage)
    {
        _storage = storage;
    }

    public async Task<IPageableResponse<EntranceBucket>> FindAllByEntranceIdAsync(int limit, int page, long entranceId)
    {
        var query = _storage.EntranceBuckets.AsQueryable();
        query = query.Where(x => x.EntranceId == entranceId);
        var count = await query.CountAsync();
        var result = await query.Skip(limit * page).Take(limit).ToListAsync();
        return new PageableResponse<EntranceBucket>(count, result);
    }

    public async Task<EntranceBucket?> FindByIdAsync(long id)
    {
        var result = await _storage.EntranceBuckets.FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }

    public async Task<EntranceBucket> CreateAsync(EntranceBucket entranceBucket)
    {
        var result = await _storage.EntranceBuckets.AddAsync(entranceBucket);
        return result.Entity;
    }

    public async Task CreateRangeAsync(IEnumerable<EntranceBucket> buckets)
    {
        await _storage.EntranceBuckets.AddRangeAsync(buckets);
    }

    public EntranceBucket Update(EntranceBucket entrance)
    {
        var result = _storage.EntranceBuckets.Update(entrance);
        return result.Entity;
    }

    public EntranceBucket Delete(EntranceBucket entrance)
    {
        var result = _storage.EntranceBuckets.Remove(entrance);
        return result.Entity;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _storage.SaveChangesAsync(cancellationToken);
    }
}