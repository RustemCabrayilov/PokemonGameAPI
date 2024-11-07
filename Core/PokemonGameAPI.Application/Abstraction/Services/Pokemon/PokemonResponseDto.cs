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
    int Defense ,
    RarityEnum RarityType ,
    Domain.Entities.Category Category,
    Domain.Entities.Skill Skill,
    int Level  = 1
);