using AutoMapper;
using MediatR;
using StorageHandler.Features.Resource.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Resource.UpdateResourceArchive;

public class UpdateResourceArchiveHandler : IRequestHandler<UpdateResourceArchiveCommand, ResourceFullDto>
{
    private readonly IResourceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ITransactionWrapper _wrapper;

    public UpdateResourceArchiveHandler(IResourceRepository repository, IMapper mapper, ITransactionWrapper wrapper)
    {
        _repository = repository;
        _mapper = mapper;
        _wrapper = wrapper;
    }

    public async Task<ResourceFullDto> Handle(UpdateResourceArchiveCommand request, CancellationToken cancellationToken)
    {
        var resource = await _wrapper.Execute(_ => UpdateResourceArchiveAsync(request, cancellationToken),
            cancellationToken);
        return _mapper.Map<ResourceFullDto>(resource);
    }

    private async Task<Resource> UpdateResourceArchiveAsync(UpdateResourceArchiveCommand request, CancellationToken cancellationToken)
    {
        var resource = await _repository.FindByIdAsync(request.Id);
        if (resource == null)
            throw ExceptionUtils.GetNotFoundException("Resource", request.Id);
        resource.ArchiveAt = resource.ArchiveAt.HasValue ? null : DateTimeOffset.UtcNow;
        var entity = _repository.Update(resource);
        await _repository.SaveChangesAsync(cancellationToken);
        return entity;
    }
}