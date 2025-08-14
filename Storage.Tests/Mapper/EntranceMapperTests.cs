using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.Entrance;
using StorageHandler.Features.Entrance.Dto;
using StorageHandler.Features.EntranceBucket;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Unit;

namespace Storage.Tests.Mapper;

public class EntranceMapperTests
{
    private readonly IMapper _mapper;

    public EntranceMapperTests()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging();
        serviceCollection.AddAutoMapper(x =>
        {
            x.AddProfile<EntranceMapper>();
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
        var entity = EntranceCreator.CreateWithBuckets(countBuckets:10);

        //Act
        var fullDto = _mapper.Map<EntranceFullDto>(entity);

        //Assert
        AssertsEntrance.Equal(entity, fullDto);
    }

    [Fact]
    public void MapToShortDto_Success()
    {
        //Arrange
        var entity = EntranceCreator.CreateWithBuckets(countBuckets: 0);

        //Act
        var fullDto = _mapper.Map<EntranceShortDto>(entity);

        //Assert
        AssertsEntrance.Equal(entity, fullDto);
    }

    [Fact]
    public void MapToEntity_Success()
    {
        //Arrange
        var entity = EntranceCreator.CreateCommand(countBuckets: 10);

        //Act
        var fullDto = _mapper.Map<Entrance>(entity);

        //Assert
        AssertsEntrance.Equal(fullDto, entity);
    }
}