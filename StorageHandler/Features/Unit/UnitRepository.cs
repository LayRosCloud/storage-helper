using Microsoft.EntityFrameworkCore;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Unit;

public class UnitRepository : IUnitRepository
{
    private readonly IStorageContext _storage;

    public UnitRepository(IStorageContext storage)
    {
        _storage = storage;
    }

    public async Task<IPageableResponse<Unit>> FindAllAsync(int limit, int page, string? name = null, bool isArchived = false)
    {
        var result = _storage.Units.AsQueryable();

        if (name != null)
        {
            result = result.Where(x => x.Name.ToLower().Contains(name.ToLower()));
        }
        
        result = result.Where(x => x.ArchiveAt.HasValue == isArchived);
        result = result.OrderBy(x => x.Name);
        var totalCount = await result.CountAsync();

        result = result.Skip(page * limit).Take(limit);

        return new PageableResponse<Unit>(totalCount, await result.ToListAsync());
    }

    public async Task<Unit?> FindByIdAsync(long id)
    {
        var result = await _storage.Units.SingleOrDefaultAsync(x => x.Id.Equals(id));
        return result;
    }

    public async Task<bool> AnyNameAsync(string name)
    {
        return await _storage.Units.AnyAsync(x => x.Name.ToLower().Equals(name.Trim().ToLower()));
    }

    public async Task<Unit> CreateAsync(Unit unit)
    {
        var result = await _storage.Units.AddAsync(unit);
        return result.Entity;
    }

    public Unit Update(Unit unit)
    {
        var result = _storage.Units.Update(unit);
        return result.Entity;
    }

    public Unit Delete(Unit unit)
    {
        var result = _storage.Units.Remove(unit);
        return result.Entity;
    }
}