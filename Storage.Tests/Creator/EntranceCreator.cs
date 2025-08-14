using StorageHandler.Features.Entrance;
using StorageHandler.Features.Entrance.CreateEntrance;
using StorageHandler.Features.EntranceBucket;
using StorageHandler.Features.EntranceBucket.CreateEntranceBucket;

namespace Storage.Tests.Creator;

public class EntranceCreator
{
    public static Entrance Create(long id = 0, string number = "", DateTimeOffset createdAt = new())
    {
        return new Entrance
        {
            Id = id,
            Number = number,
            CreatedAt = createdAt
        };
    }

    public static Entrance CreateWithBuckets(long id = 0, string number = "", DateTimeOffset createdAt = new(), int countBuckets = 1)
    {
        var list = new List<EntranceBucket>(countBuckets);
        for (var i = 0; i < countBuckets; i++)
            list.Add(BucketCreator.Create(resource: ResourceCreator.Create(i + 1), unit: UnitCreator.Create(i + 1)));

        return new Entrance
        {
            Id = id,
            Number = number,
            CreatedAt = createdAt,
            Buckets = list
        };
    }

    public static Entrance CreateWithBuckets(List<EntranceBucket> buckets, long id = 0, string number = "", DateTimeOffset createdAt = new())
    {
        return new Entrance
        {
            Id = id,
            Number = number,
            CreatedAt = createdAt,
            Buckets = buckets
        };
    }

    public static CreateEntranceCommand CreateCommand(string number = "", int countBuckets = 1)
    {
        var list = new List<CreateEntranceBucketCommand>(countBuckets);
        for (var i = 0; i < countBuckets; i++)
            list.Add(BucketCreator.CreateCommand());

        return new CreateEntranceCommand
        {
            Number = number,
            Buckets = list
        };
    }

    public static CreateEntranceCommand CreateCommand(List<CreateEntranceBucketCommand> buckets, string number = "")
    {
        return new CreateEntranceCommand
        {
            Number = number,
            Buckets = buckets
        };
    }
}