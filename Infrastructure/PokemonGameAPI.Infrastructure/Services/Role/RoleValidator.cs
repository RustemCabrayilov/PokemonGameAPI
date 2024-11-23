using FluentValidation;
using PokemonGameAPI.Application.Abstraction.Services.Role;

namespace PokemonGameAPI.Infrastructure.Services.Role;

public class RoleValidator:AbstractValidator<RoleRequestDto>
{
    public RoleValidator()
    {
        RuleFor(x=>x.Name).NotEmpty()
            .WithMessage("Name can not be empty")
            .MaximumLength(100);
    }
}