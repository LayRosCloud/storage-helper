using FluentValidation;
using Moq;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.Unit;
using StorageHandler.Features.Unit.UpdateUnitName;
using StorageHandler.Utils.Exceptions;

namespace Storage.Tests.Handlers.Units;

public class UpdateUnitNameTests : UnitsTests
{
    [Fact]
    public async Task UpdateUnit_Success()
    {
        //Arrange
        const long id = 1;
        var entity = UnitCreator.Create(id, "Unit");
        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(entity);
        Repository.Setup(x => x.Update(It.IsAny<Unit>()))
            .Returns(entity);
        SettingDefaultWrapper<Unit>(Wrapper);
        var query = new UpdateUnitNameCommand(id, "Unit1");
        var handler = new UpdateUnitNameHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        AssertsUnit.Equal(entity, result);
    }

    [Fact]
    public async Task UpdateUnit_UnitNameExists_ValidationFailure()
    {
        //Arrange
        const long id = 1;
        var entity = UnitCreator.Create(id, "Unit");
        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(entity);
        Repository.Setup(x => x.ExistsUnitByNameAsync("Unit1"))
            .ReturnsAsync(true);
        SettingDefaultWrapper<Unit>(Wrapper);
        var query = new UpdateUnitNameCommand(id, "Unit1");
        var handler = new UpdateUnitNameHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act

        //Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateUnitName_UnitIdNotExists_NotFound()
    {
        //Arrange
        const long id = 1;
        SettingDefaultWrapper<Unit>(Wrapper);
        var query = new UpdateUnitNameCommand(id, "Unit1");
        var handler = new UpdateUnitNameHandler(Repository.Object, Mapper, Wrapper.Object);
        
        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}