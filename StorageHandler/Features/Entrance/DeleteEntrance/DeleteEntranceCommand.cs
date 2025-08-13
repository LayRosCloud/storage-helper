using MediatR;
using StorageHandler.Features.Entrance.Dto;

namespace StorageHandler.Features.Entrance.DeleteEntrance;

public class DeleteEntranceCommand : IRequest<EntranceShortDto>
{
    public DeleteEntranceCommand(long id)
    {
        Id = id;
    }

    /// <summary>
    /// Id of Entrance
    /// </summary>
    public long Id { get; set; }
}