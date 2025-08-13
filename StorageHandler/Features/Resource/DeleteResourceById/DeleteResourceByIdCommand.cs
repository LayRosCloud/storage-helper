using MediatR;
using StorageHandler.Features.Resource.Dto;

namespace StorageHandler.Features.Resource.DeleteResourceById;

public class DeleteResourceByIdCommand : IRequest<ResourceFullDto>
{
    /// <summary>
    /// Id of Resource
    /// </summary>
    public long Id { get; set; }
}