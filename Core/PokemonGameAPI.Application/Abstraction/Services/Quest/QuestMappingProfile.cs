using AutoMapper;

namespace PokemonGameAPI.Application.Abstraction.Services.Quest;

public class QuestMappingProfile:Profile
{
    public QuestMappingProfile()
    {
        CreateMap<QuestRequestDto, Domain.Entities.Quest>();
        CreateMap<Domain.Entities.Quest, QuestResponseDto>();
    }
}