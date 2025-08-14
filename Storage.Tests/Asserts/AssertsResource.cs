using StorageHandler.Features.Resource;
using StorageHandler.Features.Resource.CreateResource;
using StorageHandler.Features.Resource.Dto;

namespace Storage.Tests.Asserts;

public static class AssertsResource
{
    public static void Equal(Resource extended, Resource destination)
    {
        Assert.Equal(extended.Id, destination.Id);
        Assert.Equal(extended.Name, destination.Name);
        Assert.Equal(extended.ArchiveAt, destination.ArchiveAt);
    }

    public static void Equal(Resource extended, ResourceFullDto destination)
    {
        Assert.Equal(extended.Id, destination.Id);
        Assert.Equal(extended.Name, destination.Name);
    }

    public static void Equal(CreateResourceCommand extended, Resource destination)
    {
        Assert.Equal(extended.Name, destination.Name);
    }
}