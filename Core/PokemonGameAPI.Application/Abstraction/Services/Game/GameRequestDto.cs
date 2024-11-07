namespace PokemonGameAPI.Application.Abstraction.Services.Game;

public record GameRequestDto(
    int Trainer1Goals,
    int Trainer2Goal,
    Guid Trainer1Id,
    Guid Trainer2Id);