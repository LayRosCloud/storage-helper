using Microsoft.EntityFrameworkCore;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Resource;

public class ResourceRepository : IResourceRepository
{
    private readonly IStorageContext _storage;

    public ResourceRepository(IStorageContext storage)
    {
        _storage = storage;
    }

    public async Task<IPageableResponse<Resource>> FindAllAsync(int limit, int page, string? name = null)
    {
        var query = _storage.Resources.AsQueryable();
        if (name != null)
        {
            var sentence = name.Trim().ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(sentence));
        }

        var count = query.Count();
        query = query.OrderBy(x => x.Name);
        query = query.Skip(limit * page).Take(limit);
        var result = await query.ToListAsync();
        return new PageableResponse<Resource>(count, result);
    }

    public async Task<Resource?> FindByIdAsync(long id)
    {
        var result = await _storage.Resources.FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }

    public async Task<bool> ExistsResourceByNameAsync(string name)
    {
        var result = await _storage.Resources.AnyAsync(x => x.Name.ToLower().Equals(name.Trim().ToLower()));
        return result;
    }

    public async Task<bool> ExistsResourceByIdAsync(long id)
    {
        var result = await _storage.Resources.AnyAsync(x => x.Id == id);
        return result;
    }

    public async Task<bool> ExistsResourceListByIdAsync(List<long> ids)
    {
        if (ids.Any() == false) return true;
        var result = await _storage.Resources.CountAsync(x => ids.Contains(x.Id));
        return ids.Count == result;
    }

    public async Task<Resource> CreateAsync(Resource resource)
    {
        var result = await _storage.Resources.AddAsync(resource);
        return result.Entity;
    }

    public Resource Update(Resource resource)
    {
        var result = _storage.Resources.Update(resource);
        return result.Entity;
    }

    public Resource Delete(Resource resource)
    {
        var result = _storage.Resources.Remove(resource);
        return result.Entity;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _storage.SaveChangesAsync(cancellationToken);
    }
}