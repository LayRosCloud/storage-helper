using AutoMapper;
using StorageHandler.Features.Entrance.CreateEntrance;
using StorageHandler.Features.Entrance.Dto;

namespace StorageHandler.Features.Entrance;

public class EntranceMapper : Profile
{
    public EntranceMapper()
    {
        CreateMap<Entrance, EntranceFullDto>();
        CreateMap<Entrance, EntranceShortDto>();
        CreateMap<CreateEntranceCommand, Entrance>();
    }
}