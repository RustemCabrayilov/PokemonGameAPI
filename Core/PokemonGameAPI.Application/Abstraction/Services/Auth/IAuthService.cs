using PokemonGameAPI.Application.Abstraction.Services.User;

namespace PokemonGameAPI.Application.Abstraction.Services.Auth;

public interface IAuthService
{
    Task<bool> SignInAsync(SignInRequestDto requestDto);
    Task<UserResponseDto> SignUpAsync(SignUpRequestDto requestDto);
    Task<bool> ConfirmEmailAsync(string email, string token);
}