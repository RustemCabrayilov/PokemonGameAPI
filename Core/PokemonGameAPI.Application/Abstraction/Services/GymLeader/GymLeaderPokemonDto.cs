namespace PokemonGameAPI.Application.Abstraction.Services.GymLeader;

public record GymLeaderPokemonDto(
    Guid GymleaderId,
    Guid PokemonId
    );