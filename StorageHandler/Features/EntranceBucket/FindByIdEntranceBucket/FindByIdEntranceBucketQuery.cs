using MediatR;
using StorageHandler.Features.EntranceBucket.Dto;

namespace StorageHandler.Features.EntranceBucket.FindByIdEntranceBucket;

public class FindByIdEntranceBucketQuery : IRequest<EntranceBucketFullDto>
{
    /// <summary>
    /// Id of Bucket
    /// </summary>
    public long Id { get; set; }
}