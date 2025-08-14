using Moq;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Resource.FindByIdResource;
using StorageHandler.Utils.Exceptions;

namespace Storage.Tests.Handlers.Resources;

public class FindByIdResourceTests : ResourcesTests
{
    [Fact]
    public async Task FindById_ExistsResource_Success()
    {
        //Arrange
        const int id = 1;
        var resource = ResourceCreator.Create(id, "Resource");

        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(resource);

        SettingDefaultWrapper<Resource>(Wrapper);
        var query = new FindByIdResourceQuery(id);
        var handler = new FindByIdResourceHandler(Repository.Object, Wrapper.Object, Mapper);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        AssertsResource.Equal(resource, result);
    }

    [Fact]
    public async Task FindById_NotExistsResource_NotFound()
    {
        //Arrange
        const int id = 1;

        SettingDefaultWrapper<Resource>(Wrapper);
        var query = new FindByIdResourceQuery(id);
        var handler = new FindByIdResourceHandler(Repository.Object, Wrapper.Object, Mapper);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}