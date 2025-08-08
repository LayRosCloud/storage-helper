using AutoMapper;
using StorageHandler.Features.Unit.CreateUnit;
using StorageHandler.Features.Unit.Dto;
using StorageHandler.Features.Unit.UpdateUnitName;

namespace StorageHandler.Features.Unit;

public class UnitMapper : Profile
{
    public UnitMapper()
    {
        CreateMap<Unit, UnitResponseDto>();
        CreateMap<UpdateUnitNameCommand, Unit>();
        CreateMap<CreateUnitCommand, Unit>();
    }
}