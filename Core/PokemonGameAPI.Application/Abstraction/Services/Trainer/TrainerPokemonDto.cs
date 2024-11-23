namespace PokemonGameAPI.Application.Abstraction.Services.Trainer;

public record TrainerPokemonDto(
    Guid TrainerId,
    Guid PokemonId);