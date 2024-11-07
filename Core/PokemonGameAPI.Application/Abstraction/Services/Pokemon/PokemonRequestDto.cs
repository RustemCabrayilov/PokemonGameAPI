using PokemonGameAPI.Domain.Enums;

namespace PokemonGameAPI.Application.Abstraction.Services.Pokemon;

public record PokemonRequestDto
(
     string Name ,
     int EvolutionLevel ,
     int HP ,
     int Attack ,
     int Defense ,
     RarityEnum RarityType ,
     Guid CategoryId,
     Guid SkillId ,
     int Level  = 1
);