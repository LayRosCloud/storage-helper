using MediatR;
using StorageHandler.Features.Unit.Dto;

namespace StorageHandler.Features.Unit.CreateUnit;

public class CreateUnitCommand : IRequest<UnitResponseDto>
{
    public CreateUnitCommand(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Name of Unit
    /// </summary>
    public string Name { get; }
}