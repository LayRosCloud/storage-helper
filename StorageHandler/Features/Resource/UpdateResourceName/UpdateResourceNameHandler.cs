using AutoMapper;
using FluentValidation;
using MediatR;
using StorageHandler.Features.Resource.Dto;
using StorageHandler.Features.Resource.UpdateResourceArchive;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Resource.UpdateResourceName;

public class UpdateResourceNameHandler : IRequestHandler<UpdateResourceNameCommand, ResourceFullDto>
{
    private readonly IResourceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ITransactionWrapper _wrapper;

    public UpdateResourceNameHandler(IResourceRepository repository, IMapper mapper, ITransactionWrapper wrapper)
    {
        _repository = repository;
        _mapper = mapper;
        _wrapper = wrapper;
    }

    public async Task<ResourceFullDto> Handle(UpdateResourceNameCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _wrapper.Execute(_ => UpdateResourceNameAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<ResourceFullDto>(entity);
    }

    private async Task<Resource> UpdateResourceNameAsync(UpdateResourceNameCommand request, CancellationToken cancellationToken)
    {
        var resource = await _repository.FindByIdAsync(request.Id);
        if (resource == null)
            throw ExceptionUtils.GetNotFoundException("Resource", request.Id);
        var existsResource = await _repository.ExistsResourceByNameAsync(request.Name);
        if (existsResource)
            throw new ValidationException("Resource with name exists");
        resource.Name = resource.Name.Trim();
        var entity = _repository.Update(resource);
        await _repository.SaveChangesAsync(cancellationToken);
        return entity;
    }
}