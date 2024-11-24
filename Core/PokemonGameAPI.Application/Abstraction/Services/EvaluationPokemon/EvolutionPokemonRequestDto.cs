namespace PokemonGameAPI.Application.Abstraction.Services.EvaluationPokemon;

public record EvolutionPokemonRequestDto(
    string Name,
    int Attack,
    int Defense,
    int HP,
    int EvolutionLevel,
    Guid PokemonId);