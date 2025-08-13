using AutoMapper;
using FluentValidation;
using MediatR;
using StorageHandler.Features.Entrance.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Entrance.UpdateEntrance;

public class UpdateEntranceHandler : IRequestHandler<UpdateEntranceCommand, EntranceShortDto>
{
    private readonly ITransactionWrapper _wrapper;
    private readonly IMapper _mapper;
    private readonly IEntranceRepository _repository;

    public UpdateEntranceHandler(ITransactionWrapper wrapper, IMapper mapper, IEntranceRepository repository)
    {
        _wrapper = wrapper;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<EntranceShortDto> Handle(UpdateEntranceCommand request, CancellationToken cancellationToken)
    {
        var result = await _wrapper.Execute(_ => UpdateEntranceAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<EntranceShortDto>(result);
    }

    private async Task<Entrance> UpdateEntranceAsync(UpdateEntranceCommand request, CancellationToken cancellationToken)
    {
        var entrance = await _repository.FindByIdAsync(request.Id);
        if (entrance == null)
            throw ExceptionUtils.GetNotFoundException("Entrance", request.Id);
        var existsEntrance = await _repository.ExistsEntranceByNumberAsync(request.Number);
        if (existsEntrance)
            throw new ValidationException("Error! Entrance with name exists");
        entrance.Number = request.Number.Trim();
        var result = _repository.Update(entrance);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }
}