using MediatR;
using StorageHandler.Features.Resource.Dto;

namespace StorageHandler.Features.Resource.UpdateResourceArchive;

public class UpdateResourceArchiveCommand : IRequest<ResourceFullDto>
{
    /// <summary>
    /// Id of Resource
    /// </summary>
    public long Id { get; set; }
}