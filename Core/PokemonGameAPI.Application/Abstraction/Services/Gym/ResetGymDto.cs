namespace PokemonGameAPI.Application.Abstraction.Services.Gym;

public record ResetGymDto(
    Guid Pokemon1Id,
    Guid Pokemon2Id,
    Guid GymId
);