using PokemonGameAPI.Application.Abstraction.Services.Gym;
using PokemonGameAPI.Application.Abstraction.Services.Pokemon;

namespace PokemonGameAPI.Application.Abstraction.Services.GymLeader;

public record GymLeaderResponseDto(
    Guid Id,
    string Name,
    List<GymResponseDto> Gyms,
    List<PokemonResponseDto> Pokemons=null
    );