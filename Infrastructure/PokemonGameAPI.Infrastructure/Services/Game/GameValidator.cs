using FluentValidation;
using PokemonGameAPI.Application.Abstraction.Services.Game;

namespace PokemonGameAPI.Infrastructure.Services.Game;

public class GameValidator:AbstractValidator<GameRequestDto>
{
    public GameValidator()
    {
        RuleFor(x => x.Trainer1Id).NotNull().WithMessage("Trainer1 is required");
        RuleFor(x => x.Trainer1Id).NotNull().WithMessage("Trainer2 is required");
        RuleFor(x => x.ArenaId).NotNull().WithMessage("Arena is required");
    }
}