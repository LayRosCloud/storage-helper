using Moq;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.Entrance.FindByIdEntrance;
using StorageHandler.Utils.Exceptions;
using EntranceEntity = StorageHandler.Features.Entrance.Entrance;

namespace Storage.Tests.Handlers.Entrance;

public class FindByIdEntranceTests : EntranceTests
{
    [Fact]
    public async Task FindById_ExistsEntrance_Success()
    {
        //Arrange
        const long id = 1;
        var item = EntranceCreator.Create(id);
        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(item);
        SettingDefaultWrapper<EntranceEntity>(Wrapper);
        var query = new FindByIdEntranceQuery(id);
        var handler = new FindByIdEntranceHandler(Repository.Object, Wrapper.Object, Mapper);

        //Art
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        AssertsEntrance.Equal(item, result);
    }

    [Fact]
    public async Task FindById_NotExistsEntrance_Success()
    {
        //Arrange
        const long id = 1;
        SettingDefaultWrapper<EntranceEntity>(Wrapper);
        var query = new FindByIdEntranceQuery(id);
        var handler = new FindByIdEntranceHandler(Repository.Object, Wrapper.Object, Mapper);

        //Art

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}