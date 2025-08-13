using AutoMapper;
using MediatR;
using StorageHandler.Features.Entrance;
using StorageHandler.Features.EntranceBucket.Dto;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Unit;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.EntranceBucket.CreateEntranceBucket;

public class CreateEntranceBucketHandler : IRequestHandler<CreateEntranceBucketCommand, EntranceBucketFullDto>
{
    private readonly IEntranceBucketRepository _repository;
    private readonly IEntranceRepository _entranceRepository;
    private readonly IResourceRepository _resourceRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;
    private readonly ITransactionWrapper _wrapper;

    public CreateEntranceBucketHandler(IEntranceBucketRepository repository, IMapper mapper, ITransactionWrapper wrapper, IResourceRepository resourceRepository, IUnitRepository unitRepository, IEntranceRepository entranceRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _wrapper = wrapper;
        _resourceRepository = resourceRepository;
        _unitRepository = unitRepository;
        _entranceRepository = entranceRepository;
    }

    public async Task<EntranceBucketFullDto> Handle(CreateEntranceBucketCommand request, CancellationToken cancellationToken)
    {
        var result = await _wrapper.Execute(_ => CreateAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<EntranceBucketFullDto>(result);
    }

    private async Task<EntranceBucket> CreateAsync(CreateEntranceBucketCommand request, CancellationToken cancellationToken)
    {
        await ExistsAsync(request);
        var bucket = _mapper.Map<EntranceBucket>(request);
        var result = await _repository.CreateAsync(bucket);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    private async Task ExistsAsync(CreateEntranceBucketCommand request)
    {
        var existsEntrance = await _entranceRepository.ExistsEntranceByIdAsync(request.EntranceId);
        if (existsEntrance == false)
            throw ExceptionUtils.GetNotFoundException("Entrance", request.EntranceId);

        var existsResource = await _resourceRepository.ExistsResourceByIdAsync(request.ResourceId);
        if (existsResource == false)
            throw ExceptionUtils.GetNotFoundException("Resource", request.ResourceId);

        var existsUnit = await _unitRepository.ExistsUnitByIdAsync(request.UnitId);
        if (existsUnit == false)
            throw ExceptionUtils.GetNotFoundException("Unit", request.UnitId);
    }
}