using AutoMapper;
using MediatR;
using StorageHandler.Features.Entrance.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Entrance.FindByIdEntrance;

public class FindByIdEntranceHandler : IRequestHandler<FindByIdEntranceQuery, EntranceFullDto>
{
    private readonly IEntranceRepository _repository;
    private readonly ITransactionWrapper _wrapper;
    private readonly IMapper _mapper;

    public FindByIdEntranceHandler(IEntranceRepository repository, ITransactionWrapper wrapper, IMapper mapper)
    {
        _repository = repository;
        _wrapper = wrapper;
        _mapper = mapper;
    }

    public async Task<EntranceFullDto> Handle(FindByIdEntranceQuery request, CancellationToken cancellationToken)
    {
        var entrance = await _wrapper.Execute(_ => FindByIdAsync(request), cancellationToken);
        return _mapper.Map<EntranceFullDto>(entrance);
    }

    private async Task<Entrance> FindByIdAsync(FindByIdEntranceQuery request)
    {
        var entrance = await _repository.FindByIdAsync(request.Id);
        if (entrance == null)
            throw ExceptionUtils.GetNotFoundException("Entrance", request.Id);
        return entrance;
    }
}