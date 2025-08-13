namespace StorageHandler.Features.Entrance.Dto;

public class EntranceShortDto
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
}