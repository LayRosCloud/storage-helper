using AutoMapper;
using MediatR;
using StorageHandler.Features.Entrance.Dto;
using StorageHandler.Utils.Data;
using StorageHandler.Utils.Exceptions;

namespace StorageHandler.Features.Entrance.DeleteEntrance;

public class DeleteEntranceHandler : IRequestHandler<DeleteEntranceCommand, EntranceShortDto>
{
    private readonly IEntranceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ITransactionWrapper _wrapper;

    public DeleteEntranceHandler(IEntranceRepository repository, IMapper mapper, ITransactionWrapper wrapper)
    {
        _repository = repository;
        _mapper = mapper;
        _wrapper = wrapper;
    }

    public async Task<EntranceShortDto> Handle(DeleteEntranceCommand request, CancellationToken cancellationToken)
    {
        var entrance = await _wrapper.Execute(_ => DeleteByIdAsync(request, cancellationToken), cancellationToken);
        return _mapper.Map<EntranceShortDto>(entrance);
    }

    private async Task<Entrance> DeleteByIdAsync(DeleteEntranceCommand request, CancellationToken cancellationToken)
    {
        var entrance = await _repository.FindByIdAsync(request.Id);
        if (entrance == null)
            throw ExceptionUtils.GetNotFoundException("Entrance", request.Id);

        var result = _repository.Delete(entrance);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }
}