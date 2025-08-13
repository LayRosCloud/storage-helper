namespace StorageHandler.Features.Resource.Dto;

public class ResourceFullDto
{
    /// <summary>
    /// Id of Resource
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Name of Resource
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Is Resource in archive? How?
    /// </summary>
    public DateTimeOffset? ArchiveAt { get; set; }
}