namespace PokemonGameAPI.Application.Abstraction.Services.Quest;

public record QuestResponseDto(
    Guid Id,
    string Description,
    bool Done
    );