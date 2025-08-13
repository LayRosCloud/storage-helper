using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Entrance;

public interface IEntranceRepository
{
    Task<IPageableResponse<Entrance>> FindAllAsync(int limit, int page, string? number = null);
    Task<Entrance?> FindByIdAsync(long id);
    Task<bool> ExistsEntranceByIdAsync(long id);
    Task<bool> ExistsEntranceListByIdAsync(List<long> ids);
    Task<bool> ExistsEntranceByNumberAsync(string number);
    Task<Entrance> CreateAsync(Entrance resource);
    Entrance Update(Entrance resource);
    Entrance Delete(Entrance resource);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}