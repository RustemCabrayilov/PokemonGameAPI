namespace PokemonGameAPI.Application.Abstraction.Services.Gym;

public record BossFightRequestDto(
    Guid GymId,
    Guid TrainerId,
    int TrainerDamages,
    int GymLeaderDamages
);