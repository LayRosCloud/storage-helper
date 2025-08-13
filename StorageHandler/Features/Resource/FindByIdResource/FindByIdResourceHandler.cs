using AutoMapper;
using MediatR;
using StorageHandler.Features.Resource.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Resource.FindByIdResource;

public class FindByIdResourceHandler : IRequestHandler<FindByIdResourceQuery, ResourceFullDto>
{
    private readonly IResourceRepository _repository;
    private readonly ITransactionWrapper _wrapper;
    private readonly IMapper _mapper;

    public FindByIdResourceHandler(IResourceRepository repository, ITransactionWrapper wrapper, IMapper mapper)
    {
        _repository = repository;
        _wrapper = wrapper;
        _mapper = mapper;
    }

    public async Task<ResourceFullDto> Handle(FindByIdResourceQuery request, CancellationToken cancellationToken)
    {
        var resource = await _wrapper.Execute(_ => FindByIdAsync(request), cancellationToken);
        return _mapper.Map<ResourceFullDto>(resource);
    }

    private async Task<Resource> FindByIdAsync(FindByIdResourceQuery request)
    {
        var resource = await _repository.FindByIdAsync(request.Id);
        if (resource == null)
            throw ExceptionUtils.GetNotFoundException("Resource", request.Id);
        return resource;
    }
}