using AutoMapper;
using FluentValidation;
using MediatR;
using StorageHandler.Features.Entrance;
using StorageHandler.Features.EntranceBucket.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.EntranceBucket.FindAllEntranceBucketById;

public class FindAllEntranceBucketByIdHandler : IRequestHandler<FindAllEntranceBucketByIdQuery, IPageableResponse<EntranceBucketFullDto>>
{
    private readonly ITransactionWrapper _wrapper;
    private readonly IMapper _mapper;
    private readonly IEntranceBucketRepository _repository;
    private readonly IEntranceRepository _entranceRepository;

    public FindAllEntranceBucketByIdHandler(ITransactionWrapper wrapper, IMapper mapper, IEntranceBucketRepository repository, IEntranceRepository entranceRepository)
    {
        _wrapper = wrapper;
        _mapper = mapper;
        _repository = repository;
        _entranceRepository = entranceRepository;
    }

    public async Task<IPageableResponse<EntranceBucketFullDto>> Handle(FindAllEntranceBucketByIdQuery request, CancellationToken cancellationToken)
    {
        var list = await _wrapper.Execute(_ => FindAllByEntranceIdAsync(request, cancellationToken), cancellationToken);
        return list.Map<EntranceBucketFullDto>(_mapper);
    }

    public async Task<IPageableResponse<EntranceBucket>> FindAllByEntranceIdAsync(FindAllEntranceBucketByIdQuery request, CancellationToken cancellationToken)
    {
        var existsEntrance = await _entranceRepository.ExistsEntranceByIdAsync(request.EntranceId);
        if (existsEntrance == false)
            throw ExceptionUtils.GetNotFoundException("Entrance", request.EntranceId);
        var list = await _repository.FindAllByEntranceIdAsync(request.Limit, request.Page, request.EntranceId);
        return list;
    }
}