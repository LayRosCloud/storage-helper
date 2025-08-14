using FluentValidation;
using Moq;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Resource.CreateResource;

namespace Storage.Tests.Handlers.Resources;

public class CreateResourceTests : ResourcesTests
{
    [Fact]
    public async Task CreateResource_NotExistName_Success()
    {
        //Arrange
        const int id = 1;
        var resource = ResourceCreator.Create(id, "Resource");

        Repository.Setup(x => x.CreateAsync(It.IsAny<Resource>()))
            .ReturnsAsync(resource);
        Repository.Setup(x => x.ExistsResourceByNameAsync("Resource"))
            .ReturnsAsync(false);
        SettingDefaultWrapper<Resource>(Wrapper);
        var query = new CreateResourceCommand { Name = "Resource" };
        var handler = new CreateResourceHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        AssertsResource.Equal(resource, result);
    }

    [Fact]
    public async Task CreateResource_ExistName_ValidationFailure()
    {
        //Arrange
        const int id = 1;
        var resource = ResourceCreator.Create(id, "Resource");
        Repository.Setup(x => x.ExistsResourceByNameAsync("Resource"))
            .ReturnsAsync(true);
        Repository.Setup(x => x.CreateAsync(It.IsAny<Resource>()))
            .ReturnsAsync(resource);

        SettingDefaultWrapper<Resource>(Wrapper);
        var query = new CreateResourceCommand { Name = "Resource" };
        var handler = new CreateResourceHandler(Repository.Object, Mapper, Wrapper.Object);

        //Act

        //Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, CancellationToken.None));
    }
}