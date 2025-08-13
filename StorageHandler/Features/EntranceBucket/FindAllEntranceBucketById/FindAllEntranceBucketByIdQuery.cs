using MediatR;
using StorageHandler.Features.EntranceBucket.Dto;
using StorageHandler.Utils.Data;

namespace StorageHandler.Features.EntranceBucket.FindAllEntranceBucketById;

public class FindAllEntranceBucketByIdQuery : IRequest<IPageableResponse<EntranceBucketFullDto>>
{
    public long EntranceId { get; set; }
    public int Limit { get; set; }
    public int Page { get; set; }
}