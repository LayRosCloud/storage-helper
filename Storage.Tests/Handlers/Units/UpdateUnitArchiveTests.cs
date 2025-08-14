using Moq;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.Unit;
using StorageHandler.Features.Unit.UpdateUnitArchive;
using StorageHandler.Utils.Exceptions;

namespace Storage.Tests.Handlers.Units;

public class UpdateUnitArchiveTests : UnitsTests
{
    [Fact]
    public async Task UpdateUnitArchive_UnitHasArchive_Success()
    {
        //Arrange
        const long id = 1;
        var entity = UnitCreator.Create(id, "Unit");
        var item = UnitCreator.Create(id, "Unit", DateTimeOffset.UtcNow);
        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(entity);
        Repository.Setup(x => x.Update(It.IsAny<Unit>()))
            .Returns(item);
        SettingDefaultWrapper<Unit>(Wrapper);
        var query = new UpdateUnitArchiveCommand(id);
        var handler = new UpdateUnitArchiveHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        AssertsUnit.Equal(item, result);
    }

    [Fact]
    public async Task UpdateUnitArchive_UnitNotHasArchive_Success()
    {
        //Arrange
        const long id = 1;
        var entity = UnitCreator.Create(id, "Unit", DateTimeOffset.UtcNow);
        var item = UnitCreator.Create(id, "Unit");
        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(entity);
        Repository.Setup(x => x.Update(It.IsAny<Unit>()))
            .Returns(item);
        SettingDefaultWrapper<Unit>(Wrapper);
        var query = new UpdateUnitArchiveCommand(id);
        var handler = new UpdateUnitArchiveHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        AssertsUnit.Equal(item, result);
    }

    [Fact]
    public async Task UpdateUnitArchive_UnitNotExists_NotFound()
    {
        //Arrange
        const long id = 1;
        SettingDefaultWrapper<Unit>(Wrapper);
        var query = new UpdateUnitArchiveCommand(id);
        var handler = new UpdateUnitArchiveHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}