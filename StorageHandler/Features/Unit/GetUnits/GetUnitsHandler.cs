using AutoMapper;
using MediatR;
using StorageHandler.Features.Unit.Dto;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Unit.GetUnits;

public class GetUnitsHandler : IRequestHandler<FindAllUnitsQuery, IPageableResponse<UnitResponseDto>>
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

    public async Task<IPageableResponse<UnitResponseDto>> Handle(FindAllUnitsQuery request, CancellationToken cancellationToken)
    {
        var result = await _wrapper.Execute(_ => FindAllAsync(request), cancellationToken);
        return result.Map<UnitResponseDto>(_mapper);
    }

    private async Task<IPageableResponse<Unit>> FindAllAsync(FindAllUnitsQuery request)
    {
        var items = await _repository.FindAllAsync(request.Limit, request.Page, request.Name, request.IsArchived);
        return items;
    }
}