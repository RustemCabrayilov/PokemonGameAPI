using PokemonGameAPI.Domain.Entities.Identity;

namespace PokemonGameAPI.Application.Abstraction.Services.Trainer;

public record TrainerResponseDto(
    Guid Id,
    int WinCount,
    int LooseCount,
    AppUser AppUser,
    List<Domain.Entities.Game> Games
    );