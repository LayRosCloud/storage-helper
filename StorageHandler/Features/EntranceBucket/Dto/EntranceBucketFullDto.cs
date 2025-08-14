using StorageHandler.Features.Resource.Dto;
using StorageHandler.Features.Unit.Dto;

namespace StorageHandler.Features.EntranceBucket.Dto;

public class EntranceBucketFullDto
{
    /// <summary>
    /// Id of Bucket
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Entrance Id
    /// </summary>
    public long EntranceId { get; set; }

    /// <summary>
    /// Quantity of Resources
    /// </summary>
    public double Quantity { get; set; }

    /// <summary>
    /// Resource object
    /// </summary>
    public ResourceFullDto? Resource { get; set; }

    /// <summary>
    /// Unit object
    /// </summary>
    public UnitResponseDto? Unit { get; set; }
}