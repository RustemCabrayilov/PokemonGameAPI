using AutoMapper;

namespace PokemonGameAPI.Application.Abstraction.Services.Trainer;

public class TrainerMappingProfile:Profile
{
    public TrainerMappingProfile()
    {
        CreateMap<TrainerRequestDto, Domain.Entities.Trainer>();
        CreateMap<Domain.Entities.Trainer,  TrainerResponseDto>();
        CreateMap<TrainerPokemonDto,Domain.Entities.PokemonTrainer>();
        CreateMap<TrainerBadgeDto,Domain.Entities.BadgeTrainer>();
    }
}