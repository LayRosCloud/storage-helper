using AutoMapper;
using MediatR;
using StorageHandler.Features.Resource.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Resource.DeleteResourceById;

public class DeleteResourceByIdHandler : IRequestHandler<DeleteResourceByIdCommand, ResourceFullDto>
{
    private readonly IResourceRepository _repository;
    private readonly ITransactionWrapper _wrapper;
    private readonly IMapper _mapper;

    public DeleteResourceByIdHandler(IResourceRepository repository, IMapper mapper, ITransactionWrapper wrapper)
    {
        _repository = repository;
        _mapper = mapper;
        _wrapper = wrapper;
    }

    public async Task<ResourceFullDto> Handle(DeleteResourceByIdCommand request, CancellationToken cancellationToken)
    {
        var resource = await _wrapper.Execute(_ => DeleteByIdAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<ResourceFullDto>(resource);
    }

    public async Task<Resource> DeleteByIdAsync(DeleteResourceByIdCommand request, CancellationToken cancellationToken)
    {
        var resource = await _repository.FindByIdAsync(request.Id);
        if (resource == null)
            throw ExceptionUtils.GetNotFoundException("Resource", request.Id);
        _repository.Delete(resource);
        await _repository.SaveChangesAsync(cancellationToken);
        return resource;
    }
}