using MediatR;
using StorageHandler.Features.Unit.Dto;

namespace StorageHandler.Features.Unit.DeleteUnit;

public class DeleteByIdUnitCommand : IRequest<UnitResponseDto>
{
    public DeleteByIdUnitCommand(long id)
    {
        Id = id;
    }

    /// <summary>
    /// Id of Unit
    /// </summary>
    public long Id { get; }
}