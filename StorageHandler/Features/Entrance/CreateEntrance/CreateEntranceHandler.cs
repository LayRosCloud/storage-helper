using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using StorageHandler.Features.Entrance.Dto;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Entrance.CreateEntrance;

public class CreateEntranceHandler : IRequestHandler<CreateEntranceCommand, EntranceFullDto>
{
    private readonly ITransactionWrapper _wrapper;
    private readonly IMapper _mapper;
    private readonly IEntranceRepository _repository;

    public CreateEntranceHandler(ITransactionWrapper wrapper, IMapper mapper, IEntranceRepository repository)
    {
        _wrapper = wrapper;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<EntranceFullDto> Handle(CreateEntranceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _wrapper.Execute(_ => CreateAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<EntranceFullDto>(entity);
    }

    private async Task<Entrance> CreateAsync(CreateEntranceCommand request, CancellationToken cancellationToken)
    {
        var existsEntrance = await _repository.ExistsEntranceByNumberAsync(request.Number);
        if (existsEntrance)
            throw new ValidationException("Error! Entrance with number exist");
        var entrance = _mapper.Map<Entrance>(request);
        var entity = await _repository.CreateAsync(entrance);
        await _repository.SaveChangesAsync(cancellationToken);
        return entity;
    }
}