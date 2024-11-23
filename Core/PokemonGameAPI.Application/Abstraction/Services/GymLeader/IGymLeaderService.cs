namespace PokemonGameAPI.Application.Abstraction.Services.GymLeader;

public interface IGymLeaderService
{
    Task<GymLeaderResponseDto> CreateAsync(GymLeaderRequestDto dto);
    Task<GymLeaderResponseDto> UpdateAsync(Guid id,GymLeaderRequestDto dto);
    Task<GymLeaderResponseDto> RemoveAsync(Guid id);
    Task<GymLeaderResponseDto> GetAsync(Guid id);
    Task<IList<GymLeaderResponseDto>> GetAllAsync();
    Task<GymLeaderResponseDto> AsssignPokemonAsync(GymLeaderPokemonDto dto);
}