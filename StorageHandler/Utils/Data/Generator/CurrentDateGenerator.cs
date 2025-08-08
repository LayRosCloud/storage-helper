using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using StorageHandler.Utils.Time;

namespace StorageHandler.Utils.Data.Generator;

public class CurrentDateGenerator : ValueGenerator<DateTimeOffset>
{
    public override DateTimeOffset Next(EntityEntry entry)
    {
        return CurrentTimeUtils.GetCurrentDate();
    }

    public override bool GeneratesTemporaryValues => false;
}