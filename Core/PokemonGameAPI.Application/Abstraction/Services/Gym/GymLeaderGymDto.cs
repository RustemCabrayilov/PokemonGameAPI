namespace PokemonGameAPI.Application.Abstraction.Services.Gym;

public record GymLeaderGymDto(
    Guid GymId,
    Guid GymLeaderId
    );