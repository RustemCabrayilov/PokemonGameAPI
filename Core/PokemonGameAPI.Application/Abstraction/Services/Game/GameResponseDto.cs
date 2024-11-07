using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Application.Abstraction.Services.Game;

public record GameResponseDto(
    Guid Id,
    int Trainer1Goals,
    int Trainer2Goal,
    Domain.Entities.Trainer Trainer1,
    Domain.Entities.Trainer Trainer2);