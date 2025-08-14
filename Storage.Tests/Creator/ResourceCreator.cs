using StorageHandler.Features.Resource;
using StorageHandler.Features.Resource.CreateResource;

namespace Storage.Tests.Creator;

public static class ResourceCreator
{
    public static Resource Create(long id = 0, string name = "", DateTimeOffset? archiveAt = null)
    {
        return new Resource
        {
            Id = id,
            Name = name,
            ArchiveAt = archiveAt
        };
    }

    public static CreateResourceCommand CreateCommand(string name = "")
    {
        return new CreateResourceCommand
        {
            Name = name,
        };
    }
}