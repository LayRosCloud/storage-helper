namespace StorageHandler.Features.EntranceBucket;

public class EntranceBucket
{
    /// <summary>
    /// Id of Bucket
    /// </summary>
    public long Id { get; set; }

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

    /// <summary>
    /// Resource object
    /// </summary>
    public virtual Resource.Resource? Resource { get; set; }

    /// <summary>
    /// Unit object
    /// </summary>
    public virtual Unit.Unit? Unit { get; set; }
}