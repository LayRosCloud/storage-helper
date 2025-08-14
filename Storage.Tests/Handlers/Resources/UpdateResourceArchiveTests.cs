using Moq;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Resource.UpdateResourceArchive;
using StorageHandler.Utils.Exceptions;

namespace Storage.Tests.Handlers.Resources;

public class UpdateResourceArchiveTests : ResourcesTests
{
    [Fact]
    public async Task UpdateResourceArchive_ExistsResourceNotArchived_Success()
    {
        //Arrange
        const int id = 1;
        var resource = ResourceCreator.Create(id, "Resource");

        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(resource);
        Repository.Setup(x => x.Update(It.IsAny<Resource>()))
            .Returns(resource);

        SettingDefaultWrapper<Resource>(Wrapper);
        var query = new UpdateResourceArchiveCommand(id);
        var handler = new UpdateResourceArchiveHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        AssertsResource.Equal(resource, result);
    }

    [Fact]
    public async Task UpdateResourceArchive_ExistsResourceArchived_Success()
    {
        //Arrange
        const int id = 1;
        var resource = ResourceCreator.Create(id, "Resource", DateTimeOffset.UtcNow);

        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(resource);
        Repository.Setup(x => x.Update(It.IsAny<Resource>()))
            .Returns(resource);

        SettingDefaultWrapper<Resource>(Wrapper);
        var query = new UpdateResourceArchiveCommand(id);
        var handler = new UpdateResourceArchiveHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        AssertsResource.Equal(resource, result);
    }

    [Fact]
    public async Task UpdateResourceArchive_NotExistsResource_Success()
    {
        //Arrange
        const int id = 1;
        var resource = ResourceCreator.Create(id, "Resource", DateTimeOffset.UtcNow);

        Repository.Setup(x => x.Update(It.IsAny<Resource>()))
            .Returns(resource);

        SettingDefaultWrapper<Resource>(Wrapper);
        var query = new UpdateResourceArchiveCommand(id);
        var handler = new UpdateResourceArchiveHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}