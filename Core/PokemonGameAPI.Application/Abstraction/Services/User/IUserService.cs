namespace PokemonGameAPI.Application.Abstraction.Services.User;

public interface IUserService
{
    Task<UserResponseDto> CreateAsync(UserRequestDto dto);
    Task<UserResponseDto> UpdateAsync(string id,UserRequestDto dto);
    Task<UserResponseDto> RemoveAsync(string id);
    Task<UserResponseDto> GetAsync(string id);
    Task<IList<UserResponseDto>> GetAllAsync();
    Task<UserResponseDto> AssignRoleAsync(UserRoleDto dto);
}