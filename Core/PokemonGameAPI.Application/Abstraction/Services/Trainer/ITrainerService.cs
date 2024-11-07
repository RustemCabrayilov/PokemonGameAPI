namespace PokemonGameAPI.Application.Abstraction.Services.Trainer;

public interface ITrainerService
{
    Task<TrainerResponseDto> CreateAsync(TrainerRequestDto dto);
    Task<TrainerResponseDto> UpdateAsync(Guid id,TrainerRequestDto dto);
    Task<TrainerResponseDto> RemoveAsync(Guid id);
    Task<TrainerResponseDto> GetAsync(Guid id);
    Task<IList<TrainerResponseDto>> GetAllAsync();
}