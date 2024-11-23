using AutoMapper;

namespace PokemonGameAPI.Application.Abstraction.Services.Badge;

public class BadgeMappingProfile:Profile
{
    public BadgeMappingProfile()
    {
        CreateMap<BadgeRequestDto, Domain.Entities.Badge>();
        CreateMap<Domain.Entities.Badge,BadgeResponseDto>();
    }
}