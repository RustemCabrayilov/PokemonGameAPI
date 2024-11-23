using FluentValidation;
using PokemonGameAPI.Application.Abstraction.Services.User;

namespace PokemonGameAPI.Infrastructure.Services.User;

public class UserValidator:AbstractValidator<UserRequestDto>
{
    public UserValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .Length(6, 50).WithMessage("Password must be between 6 and 50 characters");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(@"^\+994\d{9}$").WithMessage("Phone number must start with +994 and be followed by 9 digits");
    }   
}