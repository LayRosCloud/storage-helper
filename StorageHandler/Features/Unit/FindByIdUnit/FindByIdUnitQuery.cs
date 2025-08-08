using MediatR;
using StorageHandler.Features.Unit.Dto;

namespace StorageHandler.Features.Unit.FindByIdUnit;

public class FindByIdUnitQuery : IRequest<UnitResponseDto>
{
    public FindByIdUnitQuery(long id)
    {
        Id = id;
    }

    /// <summary>
    /// Id of Unit
    /// </summary>
    public long Id { get; }
}