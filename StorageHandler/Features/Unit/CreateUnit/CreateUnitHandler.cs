using AutoMapper;
using FluentValidation;
using MediatR;
using StorageHandler.Features.Unit.Dto;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Unit.CreateUnit;

public class CreateUnitHandler : IRequestHandler<CreateUnitCommand, UnitResponseDto>
{
    private readonly IStorageContext _storage;
    private readonly IUnitRepository _repository;
    private readonly IMapper _mapper;
    private readonly ITransactionWrapper _wrapper;

    public CreateUnitHandler(IStorageContext storage, IUnitRepository repository, IMapper mapper, ITransactionWrapper wrapper)
    {
        _storage = storage;
        _repository = repository;
        _mapper = mapper;
        _wrapper = wrapper;
    }

    public async Task<UnitResponseDto> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        var response = await _wrapper.Execute(() => CreateAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<UnitResponseDto>(response);
    }

    private async Task<Unit> CreateAsync(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        var hasUnitWithName = await _repository.AnyNameAsync(request.Name);

        if (hasUnitWithName)
            throw new ValidationException("Error! You don't usage this name for unit");

        var response = await _repository.CreateAsync(_mapper.Map<Unit>(request));
        await _storage.SaveChangesAsync(cancellationToken);
        return response;
    }
}