using MediatR;
using StorageHandler.Features.EntranceBucket.Dto;

namespace StorageHandler.Features.EntranceBucket.DeleteEntranceBucket;

public class DeleteEntranceBucketCommand : IRequest<EntranceBucketFullDto>
{
    /// <summary>
    /// Id of Bucket
    /// </summary>
    public long Id { get; set; }
}