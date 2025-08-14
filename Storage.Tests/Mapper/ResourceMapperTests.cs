using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Resource.Dto;
using StorageHandler.Utils.Time;

namespace Storage.Tests.Mapper;

public class ResourceMapperTests
{
    private readonly IMapper _mapper;

    public ResourceMapperTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        serviceCollection.AddAutoMapper(x => x.AddProfile(typeof(ResourceMapper)));

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
    }

    [Fact]
    public void MapToFullDto_Success()
    {
        //Arrange
        var entity = ResourceCreator.Create(1, "Resource", CurrentTimeUtils.GetCurrentDate());

        //Act
        var fullDto = _mapper.Map<ResourceFullDto>(entity);

        //Assert
        AssertsResource.Equal(entity, fullDto);
    }

    [Fact]
    public void MapToEntity_Success()
    {
        //Arrange
        var dto = ResourceCreator.CreateCommand("Resource");

        //Act
        var entity = _mapper.Map<Resource>(dto);

        //Assert
        AssertsResource.Equal(dto, entity);
    }
}