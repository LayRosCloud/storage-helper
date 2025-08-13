namespace StorageHandler.Features.Entrance;

public class Entrance
{
    /// <summary>
    /// Id of Entrance
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Number of Entrance
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// CreatedAt of Entrance
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// List of Buckets
    /// </summary>
    public virtual ICollection<EntranceBucket.EntranceBucket>? Buckets { get; set; }
}