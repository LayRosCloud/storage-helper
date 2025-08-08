using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Unit;

public interface IUnitRepository
{
    Task<IPageableResponse<Unit>> FindAllAsync(int limit, int page, string? name = null, bool isArchived = false);
    Task<Unit?> FindByIdAsync(long id);
    Task<bool> AnyNameAsync(string name);
    Task<Unit> CreateAsync(Unit unit);
    Unit Update(Unit unit);
    Unit Delete(Unit unit);
}