using FluentValidation;
using PokemonGameAPI.Application.Abstraction.Services.Skill;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Infrastructure.Services.Skill;

public class SkillValidator:AbstractValidator<SkillRequestDto>
{
    public SkillValidator()
    {
        RuleFor(skill => skill.Name).NotEmpty().WithMessage("Name cannot be empty").MaximumLength(200);
    }
}