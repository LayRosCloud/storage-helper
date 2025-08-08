using AutoMapper;
using MediatR;
using StorageHandler.Features.Unit.Dto;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Unit.GetUnits;

public class GetUnitsHandler : IRequestHandler<GetUnitsQuery, IPageableResponse<UnitResponseDto>>
{
    private readonly IUnitRepository _repository;
    private readonly ITransactionWrapper _wrapper;
    private readonly IMapper _mapper;

    public GetUnitsHandler(IUnitRepository repository, IMapper mapper, ITransactionWrapper wrapper)
    {
        _repository = repository;
        _mapper = mapper;
        _wrapper = wrapper;
    }

    public async Task<IPageableResponse<UnitResponseDto>> Handle(GetUnitsQuery request, CancellationToken cancellationToken)
    {
        var result = await _wrapper.Execute(() => FindAllAsync(request), cancellationToken);
        return result.Map<UnitResponseDto>(_mapper);
    }

    public async Task<IPageableResponse<Unit>> FindAllAsync(GetUnitsQuery request)
    {
        var items = await _repository.FindAllAsync(request.Limit, request.Page, request.Name, request.IsArchived);
        return items;
    }
}