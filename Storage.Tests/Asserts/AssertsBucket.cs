using StorageHandler.Features.EntranceBucket;
using StorageHandler.Features.EntranceBucket.CreateEntranceBucket;
using StorageHandler.Features.EntranceBucket.Dto;

namespace Storage.Tests.Asserts;

public static class AssertsBucket
{
    public static void Equal(EntranceBucket extended, EntranceBucket destination)
    {
        Assert.Equal(extended.Id, destination.Id);
        Assert.Equal(extended.EntranceId, destination.EntranceId);
        Assert.Equal(extended.UnitId, destination.UnitId);
        Assert.Equal(extended.ResourceId, destination.ResourceId);
        Assert.Equal(extended.Quantity, destination.Quantity);
        if (extended.Unit != null && destination.Unit != null)
        {
            AssertsUnit.Equal(extended.Unit, destination.Unit);
        }

        if (extended.Resource != null && destination.Resource != null)
        {
            AssertsResource.Equal(extended.Resource, destination.Resource);
        }
    }

    public static void Equal(EntranceBucket extended, EntranceBucketFullDto destination)
    {
        Assert.Equal(extended.Id, destination.Id);
        Assert.Equal(extended.EntranceId, destination.EntranceId);
        Assert.Equal(extended.Quantity, destination.Quantity);
        if (extended.Unit != null && destination.Unit != null)
        {
            AssertsUnit.Equal(extended.Unit, destination.Unit);
        }

        if (extended.Resource != null && destination.Resource != null)
        {
            AssertsResource.Equal(extended.Resource, destination.Resource);
        }
    }

    public static void Equal(EntranceBucket extended, CreateEntranceBucketCommand destination)
    {
        Assert.Equal(extended.EntranceId, destination.EntranceId);
        Assert.Equal(extended.UnitId, destination.UnitId);
        Assert.Equal(extended.ResourceId, destination.ResourceId);
        Assert.Equal(extended.Quantity, destination.Quantity);
    }
}