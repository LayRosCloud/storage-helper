using FluentValidation;
using Moq;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.Unit;
using StorageHandler.Features.Unit.CreateUnit;

namespace Storage.Tests.Handlers.Units;

public class CreateUnitTests : UnitsTests
{
    [Fact]
    public async Task CreateUnit_NotExistsName_Success()
    {
        //Arrange
        const long id = 1;
        var item = UnitCreator.Create(id, "Unit");
        Repository.Setup(x => x.CreateAsync(It.IsAny<Unit>()))
            .ReturnsAsync(item);
        SettingDefaultWrapper<Unit>(Wrapper);
        var query = new CreateUnitCommand("Unit");
        var handler = new CreateUnitHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        AssertsUnit.Equal(item, result);
    }

    [Fact]
    public async Task CreateUnit_ExistsName_ValidationFailure()
    {
        //Arrange
        const long id = 1;
        var item = UnitCreator.Create(id, "Unit");
        Repository.Setup(x => x.CreateAsync(It.IsAny<Unit>()))
            .ReturnsAsync(item);
        Repository.Setup(x => x.ExistsUnitByNameAsync("Unit"))
            .ReturnsAsync(true);
        SettingDefaultWrapper<Unit>(Wrapper);
        var query = new CreateUnitCommand("Unit");
        var handler = new CreateUnitHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act

        //Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, CancellationToken.None));
    }
}