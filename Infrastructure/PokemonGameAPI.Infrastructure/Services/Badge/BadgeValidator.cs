using FluentValidation;
using Microsoft.AspNetCore.Http;
using PokemonGameAPI.Application.Abstraction.Services.Badge;

namespace PokemonGameAPI.Infrastructure.Services.Badge;

public class BadgeValidator:AbstractValidator<BadgeRequestDto>
{
    public BadgeValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.File)
            .NotEmpty()
            .WithMessage("Name is required")
            .Must(x=>IsImage(x))
            .WithMessage("Only image files are allowed.");
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required").MaximumLength(100);
    }
    private bool IsImage(IFormFile file)
    {
        var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif" };
        return file != null && allowedMimeTypes.Contains(file.ContentType.ToLower());
    }
}