namespace PokemonGameAPI.Application.Abstraction.Services.Trainer;

public record TrainerRequestDto(
    int WinCount,
    int LooseCount,
    Guid AppUserId
);