using MediatR;
using StorageHandler.Features.Entrance.Dto;

namespace StorageHandler.Features.Entrance.FindByIdEntrance;

public class FindByIdEntranceQuery : IRequest<EntranceFullDto>
{
    /// <summary>
    /// Id of Entrance
    /// </summary>
    public long Id { get; set; }
}