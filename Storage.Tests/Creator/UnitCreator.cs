
using StorageHandler.Features.Unit;
using StorageHandler.Features.Unit.CreateUnit;

namespace Storage.Tests.Creator;

public static class UnitCreator
{
    public static Unit Create(long id = 0, string name = "", DateTimeOffset? archiveAt = null)
    {
        return new Unit
        {
            Id = id,
            Name = name,
            ArchiveAt = archiveAt
        };
    }

    public static CreateUnitCommand CreateCommand(string name = "")
    {
        return new CreateUnitCommand(name);
    }
}