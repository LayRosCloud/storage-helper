using MediatR;
using StorageHandler.Features.Entrance.Dto;
using StorageHandler.Features.EntranceBucket.Dto;

namespace StorageHandler.Features.EntranceBucket.UpdateEntranceBucket;

public class UpdateEntranceBucketCommand : IRequest<EntranceBucketFullDto>
{
    /// <summary>
    /// Id of Bucket
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Quantity of Resources
    /// </summary>
    public double Quantity { get; set; }
}