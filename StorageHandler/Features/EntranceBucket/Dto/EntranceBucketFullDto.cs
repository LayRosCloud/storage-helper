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
    public virtual Resource.Resource? Resource { get; set; }

    /// <summary>
    /// Unit object
    /// </summary>
    public virtual Unit.Unit? Unit { get; set; }
}