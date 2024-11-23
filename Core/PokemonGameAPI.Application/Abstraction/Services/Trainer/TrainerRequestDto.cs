namespace PokemonGameAPI.Application.Abstraction.Services.Trainer;

public record TrainerRequestDto(
    int TrainerLevel ,
    Guid UserId
);