using AutoMapper;
using Repositories.Entities;
using Services.Contracts;

namespace HistoryAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EventEntity, EventContract>()
             .ReverseMap();

            CreateMap<LevelEntity, LevelContract>()
             .ReverseMap();
        }
    }
}
