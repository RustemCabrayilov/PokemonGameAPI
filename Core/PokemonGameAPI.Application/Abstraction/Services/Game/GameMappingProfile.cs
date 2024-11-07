using AutoMapper;

namespace PokemonGameAPI.Application.Abstraction.Services.Game;

public class GameMappingProfile:Profile
{
    public GameMappingProfile()
    {
        CreateMap<GameRequestDto, GameResponseDto>();
        CreateMap<GameRequestDto, Domain.Entities.Game>();
        CreateMap<Domain.Entities.Game, GameResponseDto>();
    }
}