using MediatR;
using StorageHandler.Features.Unit.Dto;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Unit.GetUnits;

public class GetUnitsQuery : IRequest<IPageableResponse<UnitResponseDto>>
{
    /// <summary>
    /// Page for pagination
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Limit for pagination
    /// </summary>
    public int Limit { get; set; }

    /// <summary>
    /// Name contains search
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// True = only Archived; False = without Archived
    /// </summary>
    public bool IsArchived { get; set; } = false;
}