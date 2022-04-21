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

            CreateMap<EventContract, GameStateContract>();

            CreateMap<CreationContract, LevelEntity>()
                .ReverseMap();

            CreateMap<CreationEventContract, EventEntity>()
                .ReverseMap();
        }
    }
}
