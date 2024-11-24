namespace PokemonGameAPI.Application.Abstraction.Services.Quest;

public interface IQuestService
{
    Task<QuestResponseDto> CreateAsync(QuestRequestDto dto);
    Task<QuestResponseDto> UpdateAsync(Guid id,QuestRequestDto dto);
    Task<QuestResponseDto> RemoveAsync(Guid id);
    Task<QuestResponseDto> GetAsync(Guid id);
    Task<IList<QuestResponseDto>> GetAllAsync();
    Task<QuestResponseDto> CheckQuestAsync(Guid id);
    Task<QuestResponseDto> QuestUpdateAsync(Guid id);
}