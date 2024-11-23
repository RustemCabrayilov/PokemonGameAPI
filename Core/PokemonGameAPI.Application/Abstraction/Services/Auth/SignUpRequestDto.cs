namespace PokemonGameAPI.Application.Abstraction.Services.Auth;

public record SignUpRequestDto(
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword,
    string PhoneNumber
);