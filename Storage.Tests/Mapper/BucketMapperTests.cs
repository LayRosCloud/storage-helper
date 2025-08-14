using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.EntranceBucket;
using StorageHandler.Features.EntranceBucket.Dto;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Unit;

namespace Storage.Tests.Mapper;

public class BucketMapperTests
{
    private readonly IMapper _mapper;

    public BucketMapperTests()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging();
        serviceCollection.AddAutoMapper(x =>
        {
            x.AddProfile<EntranceBucketMapper>();
            x.AddProfile<ResourceMapper>();
            x.AddProfile<UnitMapper>();
        });
        var services = serviceCollection.BuildServiceProvider();
        _mapper = services.GetRequiredService<IMapper>();
    }

    [Fact]
    public void MapToFullDto_Success()
    {
        //Arrange
        var unit = UnitCreator.Create(1);
        var resource = ResourceCreator.Create(1);
        var entity = BucketCreator.Create(unit: unit, resource: resource);

        //Act
        var fullDto = _mapper.Map<EntranceBucketFullDto>(entity);

        //Assert
        AssertsBucket.Equal(entity, fullDto);
    }

    [Fact]
    public void MapToEntity_Success()
    {
        //Arrange
        var command = BucketCreator.CreateCommand(unitId: 1, resourceId: 1);

        //Act
        var entity = _mapper.Map<EntranceBucket>(command);

        //Assert
        AssertsBucket.Equal(entity, command);
    }
}