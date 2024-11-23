namespace PokemonGameAPI.Application.Abstraction.Services.User;

public record UserResponseDto(
    Guid Id,
    string UserName,
    string PhoneNumber,
    string Email,
    string LastIPAddress,
    List<string> Roles=null);