namespace PokemonGameAPI.Application.Abstraction.Services.Category;

public interface ICategoryService
{
    Task<CategoryResponseDto> CreateAsync(CategoryRequestDto dto);
    Task<CategoryResponseDto> UpdateAsync(Guid id,CategoryRequestDto dto);
    Task<CategoryResponseDto> RemoveAsync(Guid id);
    Task<CategoryResponseDto> GetAsync(Guid id);
    Task<IList<CategoryResponseDto>> GetAllAsync();
}