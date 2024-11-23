namespace PokemonGameAPI.Application.Abstraction.Services.Game;

public record GameRequestDto(
    Guid Trainer1Id,
    Guid Trainer2Id,
    Guid ArenaId);