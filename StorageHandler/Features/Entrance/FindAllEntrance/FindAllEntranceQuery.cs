using MediatR;
using StorageHandler.Features.Entrance.Dto;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Entrance.FindAllEntrance;

public class FindAllEntranceQuery : IRequest<IPageableResponse<EntranceFullDto>>
{
    public int Limit { get; set; }
    public int Page { get; set; }
    public string? Number { get; set; }
}