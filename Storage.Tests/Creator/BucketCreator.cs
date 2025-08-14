using StorageHandler.Features.EntranceBucket;
using StorageHandler.Features.EntranceBucket.CreateEntranceBucket;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Unit;

namespace Storage.Tests.Creator;

public class BucketCreator
{
    public static EntranceBucket Create(long id = 0, long entranceId = 0, double quantity = 0, Resource? resource = null, Unit? unit = null)
    {
        return new EntranceBucket
        {
            Id = id,
            EntranceId = entranceId,
            UnitId = unit?.Id ?? 0,
            ResourceId = resource?.Id ?? 0,
            Quantity = quantity,
            Unit = unit,
            Resource = resource
        };
    }
    public static EntranceBucket Create(long id = 0, long entranceId = 0, double quantity = 0, long unitId = 0, long resourceId = 0)
    {
        return new EntranceBucket
        {
            Id = id,
            EntranceId = entranceId,
            UnitId = unitId,
            ResourceId = resourceId,
            Quantity = quantity,
        };
    }

    public static CreateEntranceBucketCommand CreateCommand(long entranceId = 0, long unitId = 0, long resourceId = 0, double quantity = 0)
    {
        return new CreateEntranceBucketCommand
        {
            EntranceId = entranceId,
            UnitId = unitId,
            ResourceId = resourceId,
            Quantity = quantity,
        };
    }
}