using MediatR;
using StorageHandler.Features.Resource.Dto;

namespace StorageHandler.Features.Resource.DeleteResourceById;

public class DeleteResourceByIdCommand : IRequest<ResourceFullDto>
{
    public DeleteResourceByIdCommand(long id)
    {
        Id = id;
    }

    /// <summary>
    /// Id of Resource
    /// </summary>
    public long Id { get; set; }
}