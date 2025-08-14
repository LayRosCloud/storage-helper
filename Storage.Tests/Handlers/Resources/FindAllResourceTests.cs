using Moq;
using Storage.Tests.Creator;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Resource.FindAllResource;
using StorageHandler.Utils.Data;

namespace Storage.Tests.Handlers.Resources;

public class FindAllResourceTests : ResourcesTests
{
    [Fact]
    public async Task FindAll_Limit5Page0Resource_Success()
    {
        //Arrange
        const int count = 5;
        const int totalCount = 10;
        var list = new List<Resource>(count);
        for (var i = 0; i < count; i++)
            list.Add(ResourceCreator.Create(i + 1, "Resource" + i));

        Repository.Setup(x => x.FindAllAsync(5, 0, "Resource", false))
            .ReturnsAsync(new PageableResponse<Resource>(totalCount, list));

        SettingDefaultWrapper<IPageableResponse<Resource>>(Wrapper);
        var query = new FindAllResourcesQuery { IsArchived = false, Limit = 5, Page = 0, Name = "Resource" };
        var handler = new FindAllResourceHandler(Wrapper.Object, Mapper, Repository.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(count, result.Elements.Count);
        Assert.Equal(totalCount, result.TotalCount);
    }
}