using Moq;
using Storage.Tests.Creator;
using StorageHandler.Features.Unit;
using StorageHandler.Features.Unit.GetUnits;
using StorageHandler.Utils.Data;

namespace Storage.Tests.Handlers.Units;

public class FindAllUnitsTests : UnitsTests
{
    [Fact]
    public async Task FindAllUnits_Success()
    {
        //Arrange
        const int count = 5;
        const int totalCount = 10;
        var list = new List<Unit>(count);
        for (var i = 0; i < count; i++)
            list.Add(UnitCreator.Create(i + 1, "Unit" + i));
        Repository.Setup(x => x.FindAllAsync(5, 0, "Unit", false))
            .ReturnsAsync(new PageableResponse<Unit>(totalCount, list));
        SettingDefaultWrapper<IPageableResponse<Unit>>(Wrapper);
        var query = new FindAllUnitsQuery() { IsArchived = false, Limit = 5, Page = 0, Name = "Unit" };
        var handler = new GetUnitsHandler(Repository.Object, Mapper, Wrapper.Object);
        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(count, result.Elements.Count);
        Assert.Equal(totalCount, result.TotalCount);
    }
}