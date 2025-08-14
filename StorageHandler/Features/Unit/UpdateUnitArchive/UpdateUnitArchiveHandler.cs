using AutoMapper;
using MediatR;
using StorageHandler.Features.Unit.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;
using StorageHandler.Utils.Time;

namespace StorageHandler.Features.Unit.UpdateUnitArchive;

public class UpdateUnitArchiveHandler : IRequestHandler<UpdateUnitArchiveCommand, UnitResponseDto>
{
    private readonly IUnitRepository _repository;
    private readonly IMapper _mapper;
    private readonly ITransactionWrapper _wrapper;

    public UpdateUnitArchiveHandler(IUnitRepository repository, IMapper mapper, ITransactionWrapper wrapper)
    {
        _repository = repository;
        _mapper = mapper;
        _wrapper = wrapper;
    }

    public async Task<UnitResponseDto> Handle(UpdateUnitArchiveCommand request, CancellationToken cancellationToken)
    {
        var result = await _wrapper.Execute(_ => UpdateAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<UnitResponseDto>(result);
    }
    private async Task<Unit> UpdateAsync(UpdateUnitArchiveCommand request, CancellationToken cancellationToken)
    {
        var unit = await FindByIdAsync(request.Id);
        if (unit == null)
            throw ExceptionUtils.GetNotFoundException($"Unit with id {request.Id} is not found");

        unit.ArchiveAt = unit.ArchiveAt.HasValue ? null : CurrentTimeUtils.GetCurrentDate();

        var response = _repository.Update(unit);
        await _repository.SaveChangesAsync(cancellationToken);
        return response;
    }

    private async Task<Unit?> FindByIdAsync(long id)
    {
        var result = await _repository.FindByIdAsync(id);
        return result;
    }
}