using MediatR;
using StorageHandler.Features.Resource.Dto;

namespace StorageHandler.Features.Resource.CreateResource;

public class CreateResourceCommand : IRequest<ResourceFullDto>
{
    /// <summary>
    /// Name of Resource
    /// </summary>
    public string Name { get; set; } = string.Empty;
}