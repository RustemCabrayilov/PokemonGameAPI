using Microsoft.AspNetCore.Identity;

namespace PokemonGameAPI.Domain.Entities.Identity;

public class AppUser:IdentityUser<string>
{
    public string LastIPAddress { get; set; }
}