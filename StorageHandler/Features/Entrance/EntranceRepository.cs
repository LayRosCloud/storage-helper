using Microsoft.EntityFrameworkCore;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Entrance;

public class EntranceRepository : IEntranceRepository
{
    private readonly IStorageContext _storage;

    public EntranceRepository(IStorageContext storage)
    {
        _storage = storage;
    }


    public async Task<IPageableResponse<Entrance>> FindAllAsync(int limit, int page, string? number = null)
    {
        var query = _storage.Entrances.AsQueryable();
        if (number != null)
        {
            query = query.Where(x => x.Number.Contains(number));
        }

        var count = await query.CountAsync();
        query = query.OrderBy(x => x.Number);
        query = query.Skip(limit * page).Take(limit);
        var list = await query.ToListAsync();
        return new PageableResponse<Entrance>(count, list);
    }

    public async Task<Entrance?> FindByIdAsync(long id)
    {
        var entrance = await _storage.Entrances.FirstOrDefaultAsync(x => x.Id == id);
        return entrance;
    }

    public async Task<bool> ExistsEntranceByIdAsync(long id)
    {
        return await _storage.Entrances.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsEntranceListByIdAsync(List<long> ids)
    {
        if (ids.Any() == false) return true;

        var count = await _storage.Entrances.CountAsync(x => ids.Contains(x.Id));
        return ids.Count == count;
    }

    public async Task<bool> ExistsEntranceByNumberAsync(string number)
    {
        var exists = await _storage.Entrances.AnyAsync(x => x.Number == number);
        return exists;
    }

    public async Task<Entrance> CreateAsync(Entrance entrance)
    {
        var result = await _storage.Entrances.AddAsync(entrance);
        return result.Entity;
    }

    public Entrance Update(Entrance entrance)
    {
        var result = _storage.Entrances.Update(entrance);
        return result.Entity;
    }

    public Entrance Delete(Entrance entrance)
    {
        var result = _storage.Entrances.Remove(entrance);
        return result.Entity;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _storage.SaveChangesAsync(cancellationToken);
    }
}