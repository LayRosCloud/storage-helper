using MediatR;
using StorageHandler.Features.Resource.Dto;

namespace StorageHandler.Features.Resource.FindByIdResource;

public class FindByIdResourceQuery : IRequest<ResourceFullDto>
{
    /// <summary>
    /// Id of Resource
    /// </summary>
    public long Id { get; set; }
}