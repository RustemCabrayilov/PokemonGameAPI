namespace PokemonGameAPI.Application.Abstraction.Services.Quest;

public record QuestTrainerDto(
    bool Done,
    int CurrentDamage,
    Domain.Entities.Quest Quest
    );