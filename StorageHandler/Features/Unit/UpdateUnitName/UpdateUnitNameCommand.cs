using System.Text.Json.Serialization;
using MediatR;
using StorageHandler.Features.Unit.Dto;

namespace StorageHandler.Features.Unit.UpdateUnitName;

public class UpdateUnitNameCommand : IRequest<UnitResponseDto>
{
    public UpdateUnitNameCommand(long id, string name)
    {
        Id = id;
        Name = name;
    }

    [JsonIgnore]
    public long Id { get; set; }

    /// <summary>
    /// Name of Unit
    /// </summary>
    public string Name { get; set; }
}