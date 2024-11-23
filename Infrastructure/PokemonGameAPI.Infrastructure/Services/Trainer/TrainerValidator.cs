using FluentValidation;
using PokemonGameAPI.Application.Abstraction.Services.Trainer;

namespace PokemonGameAPI.Infrastructure.Services.Trainer;

public class TrainerValidator:AbstractValidator<TrainerRequestDto>
{
    public TrainerValidator()
    {
        RuleFor(x=>x.UserId).NotNull().WithMessage("UserId cannot be null");
        RuleFor(x=>x.TrainerLevel).NotEmpty()
            .WithMessage("TrainerLevel cannot be empty")
            .GreaterThan(0)
            .WithMessage("Trainer level can not be less than 0");
    }
}