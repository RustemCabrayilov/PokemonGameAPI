using PokemonGameAPI.Domain.Enums;

namespace PokemonGameAPI.Application.Abstraction.Services.Gym;

public record BossFightResponseDto(
    /*Domain.Entities.Gym Gym,*/
  ParticipantType Winner,
  ParticipantType Looser,
    int WinnerDamages,
    int LooserDamages
    );