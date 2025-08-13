using MediatR;
using StorageHandler.Features.Resource.Dto;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Resource.FindAllResource;

public class FindAllResourceQuery : IRequest<IPageableResponse<ResourceFullDto>>
{
    public int Limit { get; set; }
    public int Page { get; set; }
    public string? Name { get; set; }
}