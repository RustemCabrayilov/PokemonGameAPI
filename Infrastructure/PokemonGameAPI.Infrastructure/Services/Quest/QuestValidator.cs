using FluentValidation;
using PokemonGameAPI.Application.Abstraction.Services.Quest;

namespace PokemonGameAPI.Infrastructure.Services.Quest;

public class QuestValidator:AbstractValidator<QuestRequestDto>
{
    public QuestValidator()
    {
        RuleFor(x=>x.Description).NotEmpty().WithMessage("Description cannot be empty");
    }   
}