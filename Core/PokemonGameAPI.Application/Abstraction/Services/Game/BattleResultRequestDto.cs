namespace PokemonGameAPI.Application.Abstraction.Services.Game;

public record BattleResultRequestDto(
    Guid GameId,
    Guid Trainer1Id,
    Guid Trainer2Id,
    int Trainer1Damages,
    int Trainer2Damages
);