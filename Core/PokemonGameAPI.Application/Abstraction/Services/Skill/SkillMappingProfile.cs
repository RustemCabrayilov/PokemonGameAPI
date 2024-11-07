using AutoMapper;
using PokemonGameAPI.Application.Abstraction.Services.Pokemon;

namespace PokemonGameAPI.Application.Abstraction.Services.Skill;

public class SkillMappingProfile:Profile
{
    public SkillMappingProfile()
    {
        CreateMap<SkillRequestDto, SkillResponseDto>();
        CreateMap<SkillRequestDto, Domain.Entities.Skill>();
        CreateMap<Domain.Entities.Skill, SkillResponseDto>();   
    }
}