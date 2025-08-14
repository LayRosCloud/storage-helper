using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Storage.Tests.Asserts;
using Storage.Tests.Creator;
using StorageHandler.Features.Unit;
using StorageHandler.Features.Unit.Dto;
using StorageHandler.Utils.Time;

namespace Storage.Tests.Mapper;

public class UnitMapperTests
{
    private readonly IMapper _mapper;
    
    public UnitMapperTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        serviceCollection.AddAutoMapper(x => x.AddProfile(typeof(UnitMapper)));
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
    }

    [Fact]
    public void MapToFullDto_Success()
    {
        //Arrange
        var entity = UnitCreator.Create(1, "Unit", CurrentTimeUtils.GetCurrentDate());

        //Act
        var fullDto = _mapper.Map<UnitResponseDto>(entity);

        //Assert
        AssertsUnit.Equal(entity, fullDto);
    }

    [Fact]
    public void MapToEntity_Success()
    {
        //Arrange
        var dto = UnitCreator.CreateCommand("Unit");

        //Act
        var entity = _mapper.Map<Unit>(dto);

        //Assert
        AssertsUnit.Equal(dto, entity);
    }
}