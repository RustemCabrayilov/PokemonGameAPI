namespace PokemonGameAPI.Application.Abstraction.Services.Role;

public interface IRoleService
{
    Task<RoleResponseDto> CreateAsync(RoleRequestDto dto);
    Task<RoleResponseDto> UpdateAsync(string id,RoleRequestDto dto);
    Task<RoleResponseDto> RemoveAsync(string id);
    Task<RoleResponseDto> GetAsync(string id);
    Task<IList<RoleResponseDto>> GetAllAsync();
}