using MediatR;
using StorageHandler.Features.EntranceBucket.Dto;

namespace StorageHandler.Features.EntranceBucket.DeleteEntranceBucket;

public class DeleteEntranceBucketCommand : IRequest<EntranceBucketFullDto>
{
    public DeleteEntranceBucketCommand(long id)
    {
        Id = id;
    }

    /// <summary>
    /// Id of Bucket
    /// </summary>
    public long Id { get; set; }
}