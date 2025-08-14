using AutoMapper;
using FluentValidation;
using MediatR;
using StorageHandler.Features.EntranceBucket;
using StorageHandler.Features.Unit.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Unit.DeleteUnit;

public class DeleteByIdUnitHandler : IRequestHandler<DeleteByIdUnitCommand, UnitResponseDto>
{
    private readonly IMapper _mapper;
    private readonly ITransactionWrapper _wrapper;
    private readonly IUnitRepository _repository;
    private readonly IEntranceBucketRepository _bucketRepository;

    public DeleteByIdUnitHandler(IMapper mapper, IUnitRepository repository, ITransactionWrapper wrapper, IEntranceBucketRepository bucketRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _wrapper = wrapper;
        _bucketRepository = bucketRepository;
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
        if (await _bucketRepository.ExistsBucketByUnitIdAsync(id))
            throw new ValidationException("You cannot delete this unit, because they used in buckets");

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