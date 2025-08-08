namespace StorageHandler.Features.Unit;

public class Unit
{
    /// <summary>
    /// Id of Unit
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Name of Unit
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Is Unit in archive? How?
    /// </summary>
    public DateTimeOffset? ArchiveAt { get; set; }
}