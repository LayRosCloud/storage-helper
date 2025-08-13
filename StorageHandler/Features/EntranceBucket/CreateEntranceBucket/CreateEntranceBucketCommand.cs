using MediatR;
using StorageHandler.Features.Entrance.Dto;
using StorageHandler.Features.EntranceBucket.Dto;

namespace StorageHandler.Features.EntranceBucket.CreateEntranceBucket;

public class CreateEntranceBucketCommand : IRequest<EntranceBucketFullDto>
{
    /// <summary>
    /// Resource Id
    /// </summary>
    public long ResourceId { get; set; }

    /// <summary>
    /// Unit Id
    /// </summary>
    public long UnitId { get; set; }

    /// <summary>
    /// Entrance Id
    /// </summary>
    public long EntranceId { get; set; }

    /// <summary>
    /// Quantity of Resources
    /// </summary>
    public double Quantity { get; set; }
}