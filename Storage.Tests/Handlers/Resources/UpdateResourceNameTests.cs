using FluentValidation;
using Moq;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Resource.UpdateResourceName;
using StorageHandler.Utils.Exceptions;

namespace Storage.Tests.Handlers.Resources;

public class UpdateResourceNameTests : ResourcesTests
{
    [Fact]
    public async Task UpdateResourceName_NotExistsNameAndExistsResource_Success()
    {
        //Arrange
        const int id = 1;
        var resource = ResourceCreator.Create(id, "Resource");

        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(resource);
        Repository.Setup(x => x.ExistsResourceByNameAsync("Resource1"))
            .ReturnsAsync(false);
        Repository.Setup(x => x.Update(It.IsAny<Resource>()))
            .Returns(resource);

        SettingDefaultWrapper<Resource>(Wrapper);
        var query = new UpdateResourceNameCommand { Id = id, Name = "Resource1" };
        var handler = new UpdateResourceNameHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        AssertsResource.Equal(resource, result);
    }

    [Fact]
    public async Task UpdateResourceName_ExistsNameAndExistsResource_ValidationFailure()
    {
        //Arrange
        const int id = 1;
        var resource = ResourceCreator.Create(id, "Resource");

        Repository.Setup(x => x.FindByIdAsync(id))
            .ReturnsAsync(resource);
        Repository.Setup(x => x.ExistsResourceByNameAsync("Resource1"))
            .ReturnsAsync(true);
        Repository.Setup(x => x.Update(It.IsAny<Resource>()))
            .Returns(resource);

        SettingDefaultWrapper<Resource>(Wrapper);
        var query = new UpdateResourceNameCommand {Id = id, Name = "Resource1" };
        var handler = new UpdateResourceNameHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act

        //Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateResourceName_NotExistsNameAndNotExistsResource_NotFound()
    {
        //Arrange
        const int id = 1;

        SettingDefaultWrapper<Resource>(Wrapper);
        var query = new UpdateResourceNameCommand { Id = id, Name = "Resource1" };
        var handler = new UpdateResourceNameHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}