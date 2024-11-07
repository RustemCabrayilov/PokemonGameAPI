namespace PokemonGameAPI.Application.Abstraction.Services.Skill;

public interface ISkillService
{
    Task<SkillResponseDto> CreateAsync(SkillRequestDto dto);
    Task<SkillResponseDto> UpdateAsync(Guid id,SkillRequestDto dto);
    Task<SkillResponseDto> RemoveAsync(Guid id);
    Task<SkillResponseDto> GetAsync(Guid id);
    Task<IList<SkillResponseDto>> GetAllAsync();
}