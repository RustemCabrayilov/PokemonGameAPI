using AutoMapper;
namespace PokemonGameAPI.Application.Abstraction.Services.Pokemon;

public class PokemonMappingProfile:Profile
{
    public PokemonMappingProfile()
    {
        CreateMap<PokemonRequestDto, PokemonResponseDto>();
        CreateMap<PokemonRequestDto, Domain.Entities.Pokemon>();
        CreateMap<Domain.Entities.Pokemon, PokemonResponseDto>();
    }
}