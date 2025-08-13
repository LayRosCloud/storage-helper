using AutoMapper;
using MediatR;
using StorageHandler.Features.Entrance.Dto;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.Entrance.FindAllEntrance;

public class FindAllEntranceHandler : IRequestHandler<FindAllEntranceQuery, IPageableResponse<EntranceFullDto>>
{
    private readonly ITransactionWrapper _wrapper;
    private readonly IMapper _mapper;
    private readonly IEntranceRepository _repository;

    public FindAllEntranceHandler(ITransactionWrapper wrapper, IMapper mapper, IEntranceRepository repository)
    {
        _wrapper = wrapper;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IPageableResponse<EntranceFullDto>> Handle(FindAllEntranceQuery request, CancellationToken cancellationToken)
    {
        var entrances = await _wrapper.Execute(_ => FindAllAsync(request), cancellationToken);
        return entrances.Map<EntranceFullDto>(_mapper);
    }

    private async Task<IPageableResponse<Entrance>> FindAllAsync(FindAllEntranceQuery request)
    {
        var result = await _repository.FindAllAsync(request.Limit, request.Page, request.Number);
        return result;
    }
}