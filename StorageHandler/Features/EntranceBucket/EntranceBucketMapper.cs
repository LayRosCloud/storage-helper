using AutoMapper;
using StorageHandler.Features.EntranceBucket.CreateEntranceBucket;
using StorageHandler.Features.EntranceBucket.Dto;

namespace StorageHandler.Features.EntranceBucket;

public class EntranceBucketMapper : Profile
{
    public EntranceBucketMapper()
    {
        CreateMap<EntranceBucket, EntranceBucketFullDto>();
        CreateMap<CreateEntranceBucketCommand, EntranceBucket>();
    }
}