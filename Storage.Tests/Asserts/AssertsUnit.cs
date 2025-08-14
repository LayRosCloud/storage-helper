using StorageHandler.Features.Unit;
using StorageHandler.Features.Unit.CreateUnit;
using StorageHandler.Features.Unit.Dto;

namespace Storage.Tests.Asserts;

public static class AssertsUnit
{
    public static void Equal(Unit extended, Unit destination)
    {
        Assert.Equal(extended.Id, destination.Id);
        Assert.Equal(extended.Name, destination.Name);
        Assert.Equal(extended.ArchiveAt, destination.ArchiveAt);
    }

    public static void Equal(Unit extended, UnitResponseDto destination)
    {
        Assert.Equal(extended.Id, destination.Id);
        Assert.Equal(extended.Name, destination.Name);
    }

    public static void Equal(CreateUnitCommand dto, Unit destination)
    {
        Assert.Equal(dto.Name, destination.Name);
    }
}