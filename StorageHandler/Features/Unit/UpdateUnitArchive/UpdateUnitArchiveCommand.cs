using MediatR;
using StorageHandler.Features.Unit.Dto;

namespace StorageHandler.Features.Unit.UpdateUnitArchive;

public class UpdateUnitArchiveCommand : IRequest<UnitResponseDto>
{
    public UpdateUnitArchiveCommand(long id)
    {
        Id = id;
    }

    /// <summary>
    /// Id of Unit
    /// </summary>
    public long Id { get; }
}