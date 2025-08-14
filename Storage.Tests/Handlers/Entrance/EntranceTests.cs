using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StorageHandler.Features.Entrance;
using StorageHandler.Features.EntranceBucket;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Unit;
using StorageHandler.Utils.Data;

namespace Storage.Tests.Handlers.Entrance;

public abstract class EntranceTests : HandlersTests
{
    protected EntranceTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        serviceCollection.AddAutoMapper(x =>
        {
            x.AddProfile<ResourceMapper>();
            x.AddProfile<EntranceMapper>();
            x.AddProfile<EntranceBucketMapper>();
            x.AddProfile<UnitMapper>();
        });
        var provider = serviceCollection.BuildServiceProvider();
        Mapper = provider.GetRequiredService<IMapper>();
    }

    protected Mock<IEntranceRepository> Repository { get; } = new();
    protected Mock<ITransactionWrapper> Wrapper { get; } = new();
    protected IMapper Mapper { get; }
}