using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Application.Abstraction.Services.Game;

public record GameResponseDto(
    Guid Id,
    string ArenaName,
    Guid Trainer1Id,
    Guid Trainer2Id);