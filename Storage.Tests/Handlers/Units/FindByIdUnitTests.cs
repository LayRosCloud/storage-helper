using Moq;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.Unit;
using StorageHandler.Features.Unit.FindByIdUnit;
using StorageHandler.Utils.Exceptions;

namespace Storage.Tests.Handlers.Units;

public class FindByIdUnitTests : UnitsTests
{
    [Fact]
    public async Task FindByIdUnit_Success()
    {
        //Arrange
        const long id = 1;
        var item = UnitCreator.Create(id, "Unit");
        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(item);
        SettingDefaultWrapper<Unit>(Wrapper);
        var query = new FindByIdUnitQuery(id);
        var handler = new FindByIdUnitHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        AssertsUnit.Equal(item, result);
    }

    [Fact]
    public async Task FindByIdUnit_NotFound()
    {
        //Arrange
        const long id = 1;
        SettingDefaultWrapper<Unit>(Wrapper);
        var query = new FindByIdUnitQuery(id);
        var handler = new FindByIdUnitHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}