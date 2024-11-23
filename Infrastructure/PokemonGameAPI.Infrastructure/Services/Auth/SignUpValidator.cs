using FluentValidation;
using PokemonGameAPI.Application.Abstraction.Services.Auth;

namespace PokemonGameAPI.Infrastructure.Services.Auth;

public class SignUpValidator:AbstractValidator<SignUpRequestDto>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Not a valid email address");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .Length(6, 50).WithMessage("Password must be between 6 and 50 characters");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required")
            .Length(6, 50).WithMessage("Confirm password must be between 6 and 50 characters")
            .Must((x, confirmPassword) => confirmPassword == x.Password).WithMessage("Passwords do not match");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(@"^\+994\d{9}$").WithMessage("Phone number must start with +994 and be followed by 9 digits");
    }
}