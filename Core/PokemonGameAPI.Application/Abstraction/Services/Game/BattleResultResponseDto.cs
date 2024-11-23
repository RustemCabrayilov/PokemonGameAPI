namespace PokemonGameAPI.Application.Abstraction.Services.Game;

public record BattleResultResponseDto(
    Guid Id,
    Domain.Entities.Trainer Winner,
    Domain.Entities.Trainer Looser,
    int WinnerDamages,
    int LooserDamages
    );