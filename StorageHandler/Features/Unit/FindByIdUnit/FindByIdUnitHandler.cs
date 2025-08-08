using AutoMapper;
using MediatR;
using StorageHandler.Features.Unit.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Unit.FindByIdUnit;

public class FindByIdUnitHandler : IRequestHandler<FindByIdUnitQuery, UnitResponseDto>
{
    private readonly IUnitRepository _repository;
    private readonly IMapper _mapper;
    private readonly ITransactionWrapper _wrapper;

    public FindByIdUnitHandler(IUnitRepository repository, IMapper mapper, ITransactionWrapper wrapper)
    {
        _repository = repository;
        _mapper = mapper;
        _wrapper = wrapper;
    }

    public async Task<UnitResponseDto> Handle(FindByIdUnitQuery request, CancellationToken cancellationToken)
    {
        var item = await _wrapper.Execute(() => FindByIdAsync(request.Id), cancellationToken);
        return _mapper.Map<UnitResponseDto>(item);
    }

    private async Task<Unit> FindByIdAsync(long id)
    {
        var result = await _repository.FindByIdAsync(id);
        if (result == null)
            throw ExceptionUtils.GetNotFoundException($"Unit with id {id} is not found");
        return result;
    }
}