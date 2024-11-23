using PokemonGameAPI.Application.Abstraction.Services.Category;
using PokemonGameAPI.Application.Abstraction.Services.Skill;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Enums;
namespace PokemonGameAPI.Application.Abstraction.Services.Pokemon;
public record PokemonResponseDto
(
    Guid Id,
    string Name ,
    int EvolutionLevel ,
    int HP ,
    int Attack ,
    int PokemonLevel ,
    AttitudeType AttitudeType,
    bool ReadyForBattle,
    RarityEnum RarityType ,
    CategoryResponseDto Category,
    SkillResponseDto Skill
);