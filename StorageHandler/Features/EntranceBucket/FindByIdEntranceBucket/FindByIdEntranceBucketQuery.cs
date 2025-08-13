using MediatR;
using StorageHandler.Features.EntranceBucket.Dto;

namespace StorageHandler.Features.EntranceBucket.FindByIdEntranceBucket;

public class FindByIdEntranceBucketQuery : IRequest<EntranceBucketFullDto>
{
    public FindByIdEntranceBucketQuery(long id)
    {
        Id = id;
    }

    /// <summary>
    /// Id of Bucket
    /// </summary>
    public long Id { get; set; }
}