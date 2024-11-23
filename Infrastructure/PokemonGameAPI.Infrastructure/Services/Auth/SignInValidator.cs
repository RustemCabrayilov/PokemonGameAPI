using FluentValidation;
using PokemonGameAPI.Application.Abstraction.Services.Auth;

namespace PokemonGameAPI.Infrastructure.Services.Auth;

public class SignInValidator:AbstractValidator<SignInRequestDto>
{
    public SignInValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .Length(6, 50).WithMessage("Password must be between 6 and 50 characters");

    }
}