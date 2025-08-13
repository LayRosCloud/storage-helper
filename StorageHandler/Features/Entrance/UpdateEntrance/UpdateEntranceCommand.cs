using MediatR;
using StorageHandler.Features.Entrance.Dto;

namespace StorageHandler.Features.Entrance.UpdateEntrance;

public class UpdateEntranceCommand : IRequest<EntranceShortDto>
{
    /// <summary>
    /// Id of Entrance
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Number of Entrance
    /// </summary>
    public string Number { get; set; } = string.Empty;
}