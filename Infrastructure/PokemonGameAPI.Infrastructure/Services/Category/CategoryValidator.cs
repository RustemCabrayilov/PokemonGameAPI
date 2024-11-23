using FluentValidation;
using PokemonGameAPI.Application.Abstraction.Services.Category;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Infrastructure.Services.Category;

public class CategoryValidator:AbstractValidator<CategoryRequestDto>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name cannot be empty").MaximumLength(250);
    }
}