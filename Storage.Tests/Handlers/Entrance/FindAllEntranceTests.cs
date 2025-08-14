using Moq;
using Storage.Tests.Creator;
using StorageHandler.Features.Entrance.FindAllEntrance;
using StorageHandler.Utils.Data;
using EntranceEntity = StorageHandler.Features.Entrance.Entrance;

namespace Storage.Tests.Handlers.Entrance;

public class FindAllEntranceTests : EntranceTests
{
    [Fact]
    public async Task FindAll_Limit5Page0Entrance_Success()
    {
        //Arrange
        const int count = 5;
        const int totalCount = 10;
        var list = new List<EntranceEntity>(count);
        for (var i = 0; i < count; i++)
            list.Add(EntranceCreator.Create(i + 1, "AB" + i));

        Repository.Setup(x => x.FindAllAsync(5, 0, "AB"))
            .ReturnsAsync(new PageableResponse<EntranceEntity>(totalCount, list));

        SettingDefaultWrapper<IPageableResponse<EntranceEntity>>(Wrapper);
        var query = new FindAllEntranceQuery { Limit = 5, Page = 0, Number = "AB" };
        var handler = new FindAllEntranceHandler(Wrapper.Object, Mapper, Repository.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(count, result.Elements.Count);
        Assert.Equal(totalCount, result.TotalCount);
    }
}