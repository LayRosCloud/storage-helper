using AutoMapper;

namespace StorageHandler.Utils.Data;

public class PageableResponse<TEntity> : IPageableResponse<TEntity> where TEntity : class
{
    public PageableResponse(long totalCount, IList<TEntity> elements)
    {
        TotalCount = totalCount;
        Elements = elements;
    }

    public long TotalCount { get; }
    public IList<TEntity> Elements { get; }

    public IPageableResponse<TNewEntity> Map<TNewEntity>(IMapper mapper) where TNewEntity : class
    {
        return new PageableResponse<TNewEntity>(TotalCount, mapper.Map<List<TNewEntity>>(Elements));
    }
}