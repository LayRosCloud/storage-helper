using AutoMapper;
using FluentValidation;
using MediatR;
using StorageHandler.Features.Resource.Dto;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Resource.CreateResource;

public class CreateResourceHandler : IRequestHandler<CreateResourceCommand, ResourceFullDto>
{
    private readonly IResourceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ITransactionWrapper _wrapper;

    public CreateResourceHandler(IResourceRepository repository, IMapper mapper, ITransactionWrapper wrapper)
    {
        _repository = repository;
        _mapper = mapper;
        _wrapper = wrapper;
    }

    public async Task<ResourceFullDto> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        var result = await _wrapper.Execute(_ => CreateAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<ResourceFullDto>(result);
    }

    private async Task<Resource> CreateAsync(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        var existsResource = await _repository.ExistsResourceByNameAsync(request.Name);
        if (existsResource)
            throw new ValidationException("Error! Resource with name exists");
        var resource = _mapper.Map<Resource>(request);
        resource.Name = GetValidResourceName(resource.Name);

        var entity = await _repository.CreateAsync(resource);
        await _repository.SaveChangesAsync(cancellationToken);
        return entity;
    }

    private static string GetValidResourceName(string name)
    {
        return name.Trim();
    }
}