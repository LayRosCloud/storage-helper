using AutoMapper;
using FluentValidation;
using MediatR;
using StorageHandler.Features.Unit.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Unit.UpdateUnitName;

public class UpdateUnitNameHandler : IRequestHandler<UpdateUnitNameCommand, UnitResponseDto>
{
    private readonly IUnitRepository _repository;
    private readonly IMapper _mapper;
    private readonly ITransactionWrapper _wrapper;

    public UpdateUnitNameHandler(IUnitRepository repository, IMapper mapper, ITransactionWrapper wrapper)
    {
        _repository = repository;
        _mapper = mapper;
        _wrapper = wrapper;
    }

    public async Task<UnitResponseDto> Handle(UpdateUnitNameCommand request, CancellationToken cancellationToken)
    {
        var response = await _wrapper.Execute(_ => UpdateAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<UnitResponseDto>(response);
    }

    private async Task<Unit> UpdateAsync(UpdateUnitNameCommand request, CancellationToken cancellationToken)
    {
        var unit = await FindByIdAsync(request.Id);
        if (unit == null)
            throw ExceptionUtils.GetNotFoundException($"Unit with id {request.Id} is not found");

        var hasUnitWithName = await _repository.ExistsUnitByNameAsync(request.Name);

        if (hasUnitWithName)
            throw new ValidationException("Error! You don't usage this name for unit");

        var response = _repository.Update(_mapper.Map<Unit>(request));
        await _repository.SaveChangesAsync(cancellationToken);
        return response;
    }

    private async Task<Unit?> FindByIdAsync(long id)
    {
        var result = await _repository.FindByIdAsync(id);
        return result;
    }
}