using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Application.Abstraction.Services.Gym;

public record GymRequestDto(
    Guid GymLeaderId,
    int GymLevel
);