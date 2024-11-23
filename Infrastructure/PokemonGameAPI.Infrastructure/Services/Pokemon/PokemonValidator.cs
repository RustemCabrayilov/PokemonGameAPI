using FluentValidation;
using PokemonGameAPI.Application.Abstraction.Services.Pokemon;

namespace PokemonGameAPI.Infrastructure.Services.Pokemon;

public class PokemonValidator:AbstractValidator<PokemonRequestDto>
{
    public PokemonValidator()
    {
        RuleFor(pokemon => pokemon.Name).NotEmpty().MaximumLength(150);
        RuleFor(pokemon => pokemon.PokemonLevel).NotEmpty().GreaterThan(0);
        RuleFor(pokemon => pokemon.EvolutionLevel).NotEmpty().GreaterThan(0);
        RuleFor(pokemon => pokemon.SkillId).NotNull();
        RuleFor(pokemon => pokemon.CategoryId).NotNull();
    }
}