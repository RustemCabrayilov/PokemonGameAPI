namespace PokemonGameAPI.Application.Abstraction.Services.User;

public record UserRequestDto(
    string UserName,
    string Password,
    string PhoneNumber,
    string Email
    );