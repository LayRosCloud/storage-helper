using MediatR;
using StorageHandler.Features.Resource.Dto;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Resource.FindAllResource;

public class FindAllResourcesQuery : IRequest<IPageableResponse<ResourceFullDto>>
{
    public int Limit { get; set; }
    public int Page { get; set; }
    public string? Name { get; set; }
}