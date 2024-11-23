using AutoMapper;

namespace PokemonGameAPI.Application.Abstraction.Services.Document;

public class DocumentMappingProfile:Profile
{
    public DocumentMappingProfile()
    {
        CreateMap<DocumentRequestDto, Domain.Entities.Document>();
        CreateMap<Domain.Entities.Document,DocumentResponseDto>();
    }   
}