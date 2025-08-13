using MediatR;
using StorageHandler.Features.Resource.Dto;

namespace StorageHandler.Features.Resource.UpdateResourceName;

public class UpdateResourceNameCommand : IRequest<ResourceFullDto>
{
    /// <summary>
    /// Id of Resource
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Name of Resource
    /// </summary>
    public string Name { get; set; } = string.Empty;
}