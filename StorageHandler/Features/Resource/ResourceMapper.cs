using AutoMapper;
using StorageHandler.Features.Resource.CreateResource;
using StorageHandler.Features.Resource.Dto;

namespace StorageHandler.Features.Resource;

public class ResourceMapper : Profile
{
    public ResourceMapper()
    {
        CreateMap<Resource, ResourceFullDto>();
        CreateMap<CreateResourceCommand, Resource>();
    }
}