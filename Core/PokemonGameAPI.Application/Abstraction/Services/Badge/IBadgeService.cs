namespace PokemonGameAPI.Application.Abstraction.Services.Badge;

public interface IBadgeService
{
    Task<BadgeResponseDto> CreateAsync(BadgeRequestDto dto);
    Task<BadgeResponseDto> UpdateAsync(Guid id,BadgeRequestDto dto);
    Task<BadgeResponseDto> RemoveAsync(Guid id);
    Task<BadgeResponseDto> GetAsync(Guid id);
    Task<IList<BadgeResponseDto>> GetAllAsync();
}