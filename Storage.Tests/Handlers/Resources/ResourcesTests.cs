using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StorageHandler.Features.Resource;
using StorageHandler.Utils.Data;

namespace Storage.Tests.Handlers.Resources;

public class ResourcesTests : HandlersTests
{
    protected ResourcesTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        serviceCollection.AddAutoMapper(x =>
        {
            x.AddProfile<ResourceMapper>();
        });
        var provider = serviceCollection.BuildServiceProvider();
        Mapper = provider.GetRequiredService<IMapper>();
    }

    protected Mock<IResourceRepository> Repository { get; } = new();
    protected Mock<ITransactionWrapper> Wrapper { get; } = new();
    protected IMapper Mapper { get; }
}