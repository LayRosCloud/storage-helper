using MediatR;
using StorageHandler.Features.Entrance.Dto;
using StorageHandler.Features.EntranceBucket.CreateEntranceBucket;

namespace StorageHandler.Features.Entrance.CreateEntrance;

public class CreateEntranceCommand : IRequest<EntranceFullDto>
{
    /// <summary>
    /// Number of Entrance
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Entrance buckets
    /// </summary>
    public List<CreateEntranceBucketCommand> Buckets { get; set; } = new();
}