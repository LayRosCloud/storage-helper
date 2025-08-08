using AutoMapper;

namespace StorageHandler.Utils.Data;

public interface IPageableResponse<TEntity> where TEntity : class
{
    public long TotalCount { get; }
    public IList<TEntity> Elements { get; }
    public IPageableResponse<TNewEntity> Map<TNewEntity>(IMapper mapper) where TNewEntity : class;
}