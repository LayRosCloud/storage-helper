using StorageHandler.Utils.Data;

namespace StorageHandler.Features.EntranceBucket;

public interface IEntranceBucketRepository
{
    Task<IPageableResponse<EntranceBucket>> FindAllByEntranceIdAsync(int limit, int page, long entranceId);
    Task<EntranceBucket?> FindByIdAsync(long id);
    Task<EntranceBucket> CreateAsync(EntranceBucket entranceBucket);
    Task CreateRangeAsync(IEnumerable<EntranceBucket> buckets);
    EntranceBucket Update(EntranceBucket entrance);
    EntranceBucket Delete(EntranceBucket entrance);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}