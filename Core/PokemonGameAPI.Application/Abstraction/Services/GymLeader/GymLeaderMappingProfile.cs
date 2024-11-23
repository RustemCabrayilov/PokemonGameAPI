using AutoMapper;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Application.Abstraction.Services.GymLeader;

public class GymLeaderMappingProfile:Profile
{
    public GymLeaderMappingProfile()
    {
        CreateMap<GymLeaderRequestDto, Domain.Entities.GymLeader>();
        CreateMap<Domain.Entities.GymLeader, GymLeaderResponseDto>();
        CreateMap<GymLeaderPokemonDto, GymLeaderPokemon>();
    }
}