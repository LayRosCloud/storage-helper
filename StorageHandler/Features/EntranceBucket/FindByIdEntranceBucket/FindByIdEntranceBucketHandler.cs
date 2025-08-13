using AutoMapper;
using MediatR;
using StorageHandler.Features.EntranceBucket.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.EntranceBucket.FindByIdEntranceBucket;

public class FindByIdEntranceBucketHandler : IRequestHandler<FindByIdEntranceBucketQuery, EntranceBucketFullDto>
{
    private readonly ITransactionWrapper _wrapper;
    private readonly IMapper _mapper;
    private readonly IEntranceBucketRepository _repository;

    public FindByIdEntranceBucketHandler(ITransactionWrapper wrapper, IMapper mapper, IEntranceBucketRepository repository)
    {
        _wrapper = wrapper;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<EntranceBucketFullDto> Handle(FindByIdEntranceBucketQuery request, CancellationToken cancellationToken)
    {
        var bucket = await _wrapper.Execute(_ => FindByIdAsync(request), cancellationToken);
        return _mapper.Map<EntranceBucketFullDto>(bucket);
    }

    public async Task<EntranceBucket> FindByIdAsync(FindByIdEntranceBucketQuery request)
    {
        var bucket = await _repository.FindByIdAsync(request.Id);
        if (bucket == null)
            throw ExceptionUtils.GetNotFoundException("Entrance", request.Id);
        return bucket;
    }
}