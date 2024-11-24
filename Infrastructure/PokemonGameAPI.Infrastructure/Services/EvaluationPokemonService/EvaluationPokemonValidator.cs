using FluentValidation;
using PokemonGameAPI.Application.Abstraction.Services.EvaluationPokemon;

namespace PokemonGameAPI.Infrastructure.Services.EvaluationPokemonService;

public class EvaluationPokemonValidator:AbstractValidator<EvolutionPokemonRequestDto>
{
    public EvaluationPokemonValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.EvolutionLevel).NotEmpty().GreaterThan(0);
        RuleFor(x => x.PokemonId).NotNull();
    }
}