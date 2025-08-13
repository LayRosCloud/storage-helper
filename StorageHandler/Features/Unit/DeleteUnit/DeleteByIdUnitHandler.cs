using AutoMapper;
using MediatR;
using StorageHandler.Features.Unit.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Unit.DeleteUnit;

public class DeleteByIdUnitHandler : IRequestHandler<DeleteByIdUnitCommand, UnitResponseDto>
{
    private readonly IMapper _mapper;
    private readonly ITransactionWrapper _wrapper;
    private readonly IUnitRepository _repository;

    public DeleteByIdUnitHandler(IMapper mapper, IUnitRepository repository, ITransactionWrapper wrapper)
    {
        _mapper = mapper;
        _repository = repository;
        _wrapper = wrapper;
    }

    public async Task<UnitResponseDto> Handle(DeleteByIdUnitCommand request, CancellationToken cancellationToken)
    {
        var result = await _wrapper.Execute(async _ => 
                await DeleteByIdAsync(request.Id, cancellationToken), cancellationToken);
        return _mapper.Map<UnitResponseDto>(result);
    }

    private async Task<Unit> DeleteByIdAsync(long id, CancellationToken cancellationToken)
    {
        var unit = await FindByIdAsync(id);

        if (unit == null)
            throw ExceptionUtils.GetNotFoundException($"Unit with id {id} is not found");

        var deletedUnit = DeleteByUnit(unit);
        await _repository.SaveChangesAsync(cancellationToken);
        return deletedUnit;
    }

    private async Task<Unit?> FindByIdAsync(long id)
    {
        return await _repository.FindByIdAsync(id);
    }

    private Unit DeleteByUnit(Unit unit)
    {
        return _repository.Delete(unit);
    }
}