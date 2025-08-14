using StorageHandler.Features.Entrance;
using StorageHandler.Features.Entrance.CreateEntrance;
using StorageHandler.Features.Entrance.Dto;

namespace Storage.Tests.Asserts;

public static class AssertsEntrance
{
    public static void Equal(Entrance extended, Entrance destination)
    {
        Assert.Equal(extended.Id, destination.Id);
        Assert.Equal(extended.Number, destination.Number);
        Assert.Equal(extended.CreatedAt, destination.CreatedAt);
        Assert.NotNull(extended.Buckets);
        Assert.NotNull(destination.Buckets);
        var extendedBuckets = extended.Buckets.ToList();
        var destinationBuckets = destination.Buckets.ToList();
        Assert.Equal(extendedBuckets.Count, destinationBuckets.Count);
        for (var i = 0; i < extended.Buckets.Count; i++)
        {
            AssertsBucket.Equal(extendedBuckets[i], destinationBuckets[i]);
        }
    }

    public static void Equal(Entrance extended, EntranceFullDto destination)
    {
        Assert.Equal(extended.Id, destination.Id);
        Assert.Equal(extended.Number, destination.Number);
        Assert.Equal(extended.CreatedAt, destination.CreatedAt);
        Assert.NotNull(extended.Buckets);
        Assert.NotNull(destination.Buckets);
        var extendedBuckets = extended.Buckets.ToList();
        var destinationBuckets = destination.Buckets.ToList();

        Assert.Equal(extendedBuckets.Count, destinationBuckets.Count);

        for (var i = 0; i < extended.Buckets.Count; i++)
        {
            AssertsBucket.Equal(extendedBuckets[i], destinationBuckets[i]);
        }
    }

    public static void Equal(Entrance extended, EntranceShortDto destination)
    {
        Assert.Equal(extended.Id, destination.Id);
        Assert.Equal(extended.Number, destination.Number);
        Assert.Equal(extended.CreatedAt, destination.CreatedAt);
    }

    public static void Equal(Entrance extended, CreateEntranceCommand destination)
    {
        Assert.Equal(extended.Number, destination.Number);
        Assert.NotNull(extended.Buckets);
        Assert.NotNull(destination.Buckets);
        var extendedBuckets = extended.Buckets.ToList();
        var destinationBuckets = destination.Buckets.ToList();

        Assert.Equal(extendedBuckets.Count, destinationBuckets.Count);

        for (var i = 0; i < extended.Buckets.Count; i++)
        {
            AssertsBucket.Equal(extendedBuckets[i], destinationBuckets[i]);
        }
    }
}