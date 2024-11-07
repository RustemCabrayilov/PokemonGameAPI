using AutoMapper;

namespace PokemonGameAPI.Application.Abstraction.Services.Category;

public class CategoryMappingProfile:Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryRequestDto, CategoryResponseDto>();
        CreateMap<CategoryRequestDto, Domain.Entities.Category>();
        CreateMap<Domain.Entities.Category, CategoryResponseDto>();
    }
}