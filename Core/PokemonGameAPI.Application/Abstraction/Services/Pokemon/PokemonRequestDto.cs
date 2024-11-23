using PokemonGameAPI.Domain.Enums;

namespace PokemonGameAPI.Application.Abstraction.Services.Pokemon;

public record PokemonRequestDto
(
     string Name ,
     int EvolutionLevel ,
     RarityEnum RarityType ,
     Guid CategoryId,
     Guid SkillId ,
     int PokemonLevel  = 1
);