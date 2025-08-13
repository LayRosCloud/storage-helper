using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Unit;

public interface IUnitRepository
{
    Task<IPageableResponse<Unit>> FindAllAsync(int limit, int page, string? name = null, bool isArchived = false);
    Task<Unit?> FindByIdAsync(long id);
    Task<bool> ExistsUnitByIdAsync(long id);
    Task<bool> ExistsUnitListByIdAsync(List<long> ids);
    Task<bool> ExistsUnitByNameAsync(string name);
    Task<Unit> CreateAsync(Unit unit);
    Unit Update(Unit unit);
    Unit Delete(Unit unit);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}