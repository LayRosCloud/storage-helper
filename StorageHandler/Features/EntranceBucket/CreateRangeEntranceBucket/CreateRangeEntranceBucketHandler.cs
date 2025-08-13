using AutoMapper;
using MediatR;
using StorageHandler.Features.Entrance;
using StorageHandler.Features.EntranceBucket.CreateEntranceBucket;
using StorageHandler.Features.EntranceBucket.Dto;
using StorageHandler.Features.Resource;
using StorageHandler.Features.Unit;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.EntranceBucket.CreateRangeEntranceBucket;

public class CreateRangeEntranceBucketHandler : IRequestHandler<CreateRangeEntranceBucketCommand, ICollection<EntranceBucketFullDto>>
{
    private readonly IEntranceBucketRepository _repository;
    private readonly IEntranceRepository _entranceRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly IResourceRepository _resourceRepository;
    private readonly IMapper _mapper;
    private readonly ITransactionWrapper _wrapper;

    public CreateRangeEntranceBucketHandler(IEntranceBucketRepository repository, IMapper mapper, ITransactionWrapper wrapper, IEntranceRepository entranceRepository, IUnitRepository unitRepository, IResourceRepository resourceRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _wrapper = wrapper;
        _entranceRepository = entranceRepository;
        _unitRepository = unitRepository;
        _resourceRepository = resourceRepository;
    }

    public async Task<ICollection<EntranceBucketFullDto>> Handle(CreateRangeEntranceBucketCommand request, CancellationToken cancellationToken)
    {
        var result = await _wrapper.Execute(_ => CreateRangeAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<List<EntranceBucketFullDto>>(result);
    }

    private async Task<ICollection<EntranceBucket>> CreateRangeAsync(CreateRangeEntranceBucketCommand request, CancellationToken cancellationToken)
    {
        var bucketsFromCommand = request.Buckets;
        await ExistsIdsAsync(bucketsFromCommand);
        var buckets = _mapper.Map<List<EntranceBucket>>(request.Buckets);
        await _repository.CreateRangeAsync(buckets);
        await _repository.SaveChangesAsync(cancellationToken);
        return buckets;
    }

    private async Task ExistsIdsAsync(ICollection<CreateEntranceBucketCommand> buckets)
    {
        var idsEntrances = buckets.Select(x => x.UnitId).ToList();
        var existsEntrances =
            await _entranceRepository.ExistsEntranceListByIdAsync(idsEntrances);
        if (!existsEntrances)
            throw ExceptionUtils.GetNotFoundException("Entrances", string.Join(", ", idsEntrances));

        var idsUnits = buckets.Select(x => x.UnitId).ToList();
        var existsUnits =
            await _unitRepository.ExistsUnitListByIdAsync(idsUnits);
        if (!existsUnits)
            throw ExceptionUtils.GetNotFoundException("Units", string.Join(", ", idsUnits));

        var idsResources = buckets.Select(x => x.ResourceId).ToList();
        var existsResources =
            await _resourceRepository.ExistsResourceListByIdAsync(idsResources);
        if (!existsResources)
            throw ExceptionUtils.GetNotFoundException("Resources", string.Join(", ", idsResources));
    }
}