using AutoMapper;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Application.Abstraction.Services.Gym;

public class GymMappingProfile : Profile
{
    public GymMappingProfile()
    {
        CreateMap<GymRequestDto, Domain.Entities.Gym>();
        CreateMap<Domain.Entities.Gym, GymResponseDto>()
            .ForMember(dest => dest.GymFieldName, opt => opt.MapFrom(src => src.GymField.Name));
        ;
        CreateMap<Domain.Entities.BossFightResult, BossFightResponseDto>();
    }
}