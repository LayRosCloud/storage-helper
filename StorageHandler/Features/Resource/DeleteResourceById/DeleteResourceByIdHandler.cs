using AutoMapper;
using FluentValidation;
using MediatR;
using StorageHandler.Features.EntranceBucket;
using StorageHandler.Features.Resource.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Resource.DeleteResourceById;

public class DeleteResourceByIdHandler : IRequestHandler<DeleteResourceByIdCommand, ResourceFullDto>
{
    private readonly IResourceRepository _repository;
    private readonly ITransactionWrapper _wrapper;
    private readonly IEntranceBucketRepository _bucketRepository;
    private readonly IMapper _mapper;

    public DeleteResourceByIdHandler(IResourceRepository repository, IMapper mapper, ITransactionWrapper wrapper, IEntranceBucketRepository bucketRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _wrapper = wrapper;
        _bucketRepository = bucketRepository;
    }

    public async Task<ResourceFullDto> Handle(DeleteResourceByIdCommand request, CancellationToken cancellationToken)
    {
        var resource = await _wrapper.Execute(_ => DeleteByIdAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<ResourceFullDto>(resource);
    }

    private async Task<Resource> DeleteByIdAsync(DeleteResourceByIdCommand request, CancellationToken cancellationToken)
    {
        var resource = await _repository.FindByIdAsync(request.Id);
        if (resource == null)
            throw ExceptionUtils.GetNotFoundException("Resource", request.Id);
        if (await _bucketRepository.ExistsBucketByResourceIdAsync(request.Id))
            throw new ValidationException("You cannot delete this resource, because they used in buckets");
        _repository.Delete(resource);
        await _repository.SaveChangesAsync(cancellationToken);
        return resource;
    }
}