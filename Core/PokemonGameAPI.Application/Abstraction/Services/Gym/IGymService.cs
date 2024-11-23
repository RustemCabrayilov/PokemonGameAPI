namespace PokemonGameAPI.Application.Abstraction.Services.Gym;

public interface IGymService
{
    Task<GymResponseDto> CreateAsync(GymRequestDto dto);
    Task<GymResponseDto> UpdateAsync(Guid id,GymRequestDto dto);
    Task<GymResponseDto> RemoveAsync(Guid id);
    Task<GymResponseDto> GetAsync(Guid id);
    Task<IList<GymResponseDto>> GetAllAsync();
    Task<GymResponseDto> StartBattleAsync(Guid mainTrainerId,Guid gymId);
    Task<BossFightResponseDto> BossFight(BossFightRequestDto requestDto); 
    ValueTask<bool> ResetAsync(ResetGymDto requestDto);
}