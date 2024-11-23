namespace PokemonGameAPI.Application.Abstraction.Services.Auth;

public record SignInResponseDto(
    string Message,
    bool Success
    );