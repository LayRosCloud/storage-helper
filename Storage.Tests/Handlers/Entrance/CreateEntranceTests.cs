using FluentValidation;
using Moq;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.Entrance.CreateEntrance;
using EntranceEntity = StorageHandler.Features.Entrance.Entrance;

namespace Storage.Tests.Handlers.Entrance;

public class CreateEntranceTests : EntranceTests
{
    [Fact]
    public async Task CreateEntrance_NotExistsNumber_Success()
    {
        //Arrange
        const long id = 1;
        var entity = EntranceCreator.Create(id, "AB");
        Repository.Setup(x => x.CreateAsync(It.IsAny<EntranceEntity>()))
            .ReturnsAsync(entity);
        Repository.Setup(x => x.ExistsEntranceByNumberAsync("AB"))
            .ReturnsAsync(false);
        SettingDefaultWrapper<EntranceEntity>(Wrapper);
        var command = EntranceCreator.CreateCommand(countBuckets: 0);
        var handler = new CreateEntranceHandler(Wrapper.Object, Mapper, Repository.Object);
        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        //Assert
        Assert.NotNull(result);
        AssertsEntrance.Equal(entity, result);
    }

    [Fact]
    public async Task CreateEntrance_ExistsNumber_Success()
    {
        //Arrange
        const long id = 1;
        var entity = EntranceCreator.Create(id, "AB");
        Repository.Setup(x => x.CreateAsync(It.IsAny<EntranceEntity>()))
            .ReturnsAsync(entity);
        Repository.Setup(x => x.ExistsEntranceByNumberAsync("AB"))
            .ReturnsAsync(true);
        SettingDefaultWrapper<EntranceEntity>(Wrapper);
        var command = EntranceCreator.CreateCommand(number:"AB",countBuckets: 0);
        var handler = new CreateEntranceHandler(Wrapper.Object, Mapper, Repository.Object);
        //Act

        //Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
    }
}