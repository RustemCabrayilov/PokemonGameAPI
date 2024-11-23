namespace PokemonGameAPI.Application.Abstraction.Services.Game;

public record ResetGameDto(
    Guid Pokemon1Id,
    Guid Pokemon2Id,
    Guid GameId
);