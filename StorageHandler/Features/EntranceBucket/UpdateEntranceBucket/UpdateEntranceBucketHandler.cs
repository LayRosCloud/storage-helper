using AutoMapper;
using MediatR;
using StorageHandler.Features.EntranceBucket.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.EntranceBucket.UpdateEntranceBucket;

public class UpdateEntranceBucketHandler : IRequestHandler<UpdateEntranceBucketCommand, EntranceBucketFullDto>
{
    private readonly ITransactionWrapper _wrapper;
    private readonly IMapper _mapper;
    private readonly IEntranceBucketRepository _repository;

    public UpdateEntranceBucketHandler(ITransactionWrapper wrapper, IMapper mapper, IEntranceBucketRepository repository)
    {
        _wrapper = wrapper;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<EntranceBucketFullDto> Handle(UpdateEntranceBucketCommand request, CancellationToken cancellationToken)
    {
        var entity = await _wrapper.Execute(_ => UpdateEntranceBucketAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<EntranceBucketFullDto>(entity);
    }

    private async Task<EntranceBucket> UpdateEntranceBucketAsync(UpdateEntranceBucketCommand request, CancellationToken cancellationToken)
    {
        var entrance = await _repository.FindByIdAsync(request.Id);
        if (entrance == null)
            throw ExceptionUtils.GetNotFoundException("Entrance", request.Id);
        entrance.Quantity = request.Quantity;
        var entity = _repository.Update(entrance);
        await _repository.SaveChangesAsync(cancellationToken);
        return entity;
    }
}