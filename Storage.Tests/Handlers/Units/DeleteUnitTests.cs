using FluentValidation;
using Moq;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.EntranceBucket;
using StorageHandler.Features.Unit;
using StorageHandler.Features.Unit.DeleteUnit;
using StorageHandler.Utils.Exceptions;

namespace Storage.Tests.Handlers.Units;

public class DeleteUnitTests : UnitsTests
{
    private readonly Mock<IEntranceBucketRepository> _bucketRepositoryMock = new();

    [Fact]
    public async Task DeleteUnit_UnitExistsAndNotExistsInBucket_Success()
    {
        //Arrange
        const long id = 1;
        var item = UnitCreator.Create(id, "Unit");
        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(item);
        Repository.Setup(x => x.Delete(It.IsAny<Unit>()))
            .Returns(item);
        _bucketRepositoryMock.Setup(x => x.ExistsBucketByUnitIdAsync(id))
            .ReturnsAsync(false);
        SettingDefaultWrapper<Unit>(Wrapper);
        var query = new DeleteByIdUnitCommand(id);
        var handler = new DeleteByIdUnitHandler(Mapper, Repository.Object, Wrapper.Object, _bucketRepositoryMock.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        AssertsUnit.Equal(item, result);
    }

    [Fact]
    public async Task DeleteUnit_UnitExistsAndExistsInBucket_ValidationFailure()
    {
        //Arrange
        const long id = 1;
        var item = UnitCreator.Create(id, "Unit");
        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(item);
        Repository.Setup(x => x.Delete(It.IsAny<Unit>()))
            .Returns(item);
        _bucketRepositoryMock.Setup(x => x.ExistsBucketByUnitIdAsync(id))
            .ReturnsAsync(true);
        SettingDefaultWrapper<Unit>(Wrapper);
        var query = new DeleteByIdUnitCommand(id);
        var handler = new DeleteByIdUnitHandler(Mapper, Repository.Object, Wrapper.Object, _bucketRepositoryMock.Object);

        //Act

        //Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteUnit_UnitNotExistsAndExistsInBucket_NotFound()
    {
        //Arrange
        const long id = 1;
        var item = UnitCreator.Create(id, "Unit");
        Repository.Setup(x => x.Delete(It.IsAny<Unit>()))
            .Returns(item);
        _bucketRepositoryMock.Setup(x => x.ExistsBucketByUnitIdAsync(id))
            .ReturnsAsync(true);
        SettingDefaultWrapper<Unit>(Wrapper);
        var query = new DeleteByIdUnitCommand(id);
        var handler = new DeleteByIdUnitHandler(Mapper, Repository.Object, Wrapper.Object, _bucketRepositoryMock.Object);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}