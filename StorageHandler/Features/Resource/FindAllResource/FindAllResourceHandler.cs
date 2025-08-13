using AutoMapper;
using MediatR;
using StorageHandler.Features.Resource.Dto;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Resource.FindAllResource;

public class FindAllResourceHandler : IRequestHandler<FindAllResourceQuery, IPageableResponse<ResourceFullDto>>
{
    private readonly ITransactionWrapper _wrapper;
    private readonly IMapper _mapper;
    private readonly IResourceRepository _repository;

    public FindAllResourceHandler(ITransactionWrapper wrapper, IMapper mapper, IResourceRepository repository)
    {
        _wrapper = wrapper;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IPageableResponse<ResourceFullDto>> Handle(FindAllResourceQuery request, CancellationToken cancellationToken)
    {
        var resources = await _wrapper.Execute(_ => FindAllAsync(request), cancellationToken);
        return resources.Map<ResourceFullDto>(_mapper);
    }

    private async Task<IPageableResponse<Resource>> FindAllAsync(FindAllResourceQuery request)
    {
        var resources = await _repository.FindAllAsync(request.Limit, request.Page, request.Name);
        return resources;
    }
}