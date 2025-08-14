using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StorageHandler.Features.Unit;
using StorageHandler.Utils.Data;

namespace Storage.Tests.Handlers.Units;

public abstract class UnitsTests : HandlersTests
{
    protected UnitsTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        serviceCollection.AddAutoMapper(x =>
        {
            x.AddProfile<UnitMapper>();
        });
        var provider = serviceCollection.BuildServiceProvider();
        Mapper = provider.GetRequiredService<IMapper>();
    }

    protected Mock<IUnitRepository> Repository { get; } = new();
    protected Mock<ITransactionWrapper> Wrapper { get; } = new();
    protected IMapper Mapper { get; }
}