using MediatR;
using StorageHandler.Features.EntranceBucket.CreateEntranceBucket;
using StorageHandler.Features.EntranceBucket.Dto;

namespace StorageHandler.Features.EntranceBucket.CreateRangeEntranceBucket;

public class CreateRangeEntranceBucketCommand : IRequest<ICollection<EntranceBucketFullDto>>
{
    public CreateRangeEntranceBucketCommand(ICollection<CreateEntranceBucketCommand> buckets)
    {
        Buckets = buckets;
    }

    public ICollection<CreateEntranceBucketCommand> Buckets { get; set; }
}