using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Resource;

public interface IResourceRepository
{
    Task<IPageableResponse<Resource>> FindAllAsync(int limit, int page, string? name = null);
    Task<Resource?> FindByIdAsync(long id);
    Task<bool> ExistsResourceByNameAsync(string name);
    Task<bool> ExistsResourceByIdAsync(long id);
    Task<bool> ExistsResourceListByIdAsync(List<long> ids);
    Task<Resource> CreateAsync(Resource resource);
    Resource Update(Resource resource);
    Resource Delete(Resource resource);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}