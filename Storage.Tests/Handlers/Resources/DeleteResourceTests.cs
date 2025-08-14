using FluentValidation;
using Moq;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.EntranceBucket;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Resource.DeleteResourceById;
using StorageHandler.Utils.Exceptions;

namespace Storage.Tests.Handlers.Resources;

public class DeleteResourceTests : ResourcesTests
{
    private readonly Mock<IEntranceBucketRepository> _bucketRepositoryMock = new();

    [Fact]
    public async Task DeleteResource_ExistsResourceNotExistsInBuckets_Success()
    {
        //Arrange
        const long id = 1;
        var item = ResourceCreator.Create(id, "Resource");
        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(item);
        Repository.Setup(x => x.Delete(It.IsAny<Resource>()))
            .Returns(item);
        _bucketRepositoryMock.Setup(x => x.ExistsBucketByResourceIdAsync(id))
            .ReturnsAsync(false);
        SettingDefaultWrapper<Resource>(Wrapper);
        var query = new DeleteResourceByIdCommand(id);
        var handler = new DeleteResourceByIdHandler(Repository.Object, Mapper, Wrapper.Object, _bucketRepositoryMock.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        AssertsResource.Equal(item, result);
    }

    [Fact]
    public async Task DeleteResource_ExistsResourceExistsInBuckets_Success()
    {
        //Arrange
        const long id = 1;
        var item = ResourceCreator.Create(id, "Resource");
        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(item);
        Repository.Setup(x => x.Delete(It.IsAny<Resource>()))
            .Returns(item);
        _bucketRepositoryMock.Setup(x => x.ExistsBucketByResourceIdAsync(id))
            .ReturnsAsync(true);
        SettingDefaultWrapper<Resource>(Wrapper);
        var query = new DeleteResourceByIdCommand(id);
        var handler = new DeleteResourceByIdHandler(Repository.Object, Mapper, Wrapper.Object, _bucketRepositoryMock.Object);

        //Act

        //Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteResource_NotExistsResourceNotExistsInBuckets_Success()
    {
        //Arrange
        const long id = 1;
        var item = ResourceCreator.Create(id, "Resource");
        Repository.Setup(x => x.Delete(It.IsAny<Resource>()))
            .Returns(item);
        _bucketRepositoryMock.Setup(x => x.ExistsBucketByResourceIdAsync(id))
            .ReturnsAsync(true);
        SettingDefaultWrapper<Resource>(Wrapper);
        var query = new DeleteResourceByIdCommand(id);
        var handler = new DeleteResourceByIdHandler(Repository.Object, Mapper, Wrapper.Object, _bucketRepositoryMock.Object);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}