namespace PokemonGameAPI.Application.Abstraction.Services.EvaluationPokemon;

public record EvolutionPokemonResponseDto(
    Guid Id,
    string Name,
    int Attack,
    int Defense,
    int HP,
    int EvolutionLevel,
    Domain.Entities.Pokemon Pokemon
);