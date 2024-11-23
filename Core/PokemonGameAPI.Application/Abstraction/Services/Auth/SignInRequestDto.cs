namespace PokemonGameAPI.Application.Abstraction.Services.Auth;

public record SignInRequestDto(string UserName, string Password
,bool IsPersistent);