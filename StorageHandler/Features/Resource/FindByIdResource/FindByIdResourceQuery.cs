using MediatR;
using StorageHandler.Features.Resource.Dto;

namespace StorageHandler.Features.Resource.FindByIdResource;

public class FindByIdResourceQuery : IRequest<ResourceFullDto>
{
    public FindByIdResourceQuery(long id)
    {
        Id = id;
    }

    /// <summary>
    /// Id of Resource
    /// </summary>
    public long Id { get; set; }
}