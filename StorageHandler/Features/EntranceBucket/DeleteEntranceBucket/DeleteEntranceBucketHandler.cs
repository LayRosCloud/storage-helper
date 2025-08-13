using AutoMapper;
using MediatR;
using StorageHandler.Features.EntranceBucket.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.EntranceBucket.DeleteEntranceBucket;

public class DeleteEntranceBucketHandler : IRequestHandler<DeleteEntranceBucketCommand, EntranceBucketFullDto>
{
    private readonly ITransactionWrapper _wrapper;
    private readonly IMapper _mapper;
    private readonly IEntranceBucketRepository _repository;

    public DeleteEntranceBucketHandler(ITransactionWrapper wrapper, IMapper mapper, IEntranceBucketRepository repository)
    {
        _wrapper = wrapper;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<EntranceBucketFullDto> Handle(DeleteEntranceBucketCommand request, CancellationToken cancellationToken)
    {
        var entrance = await _wrapper.Execute(_ => DeleteByIdAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<EntranceBucketFullDto>(entrance);
    }

    private async Task<EntranceBucket> DeleteByIdAsync(DeleteEntranceBucketCommand request, CancellationToken cancellationToken)
    {
        var bucket = await _repository.FindByIdAsync(request.Id);
        if (bucket == null)
            throw ExceptionUtils.GetNotFoundException("Bucket", request.Id);
        var entity = _repository.Delete(bucket);
        await _repository.SaveChangesAsync(cancellationToken);
        return entity;
    }
}