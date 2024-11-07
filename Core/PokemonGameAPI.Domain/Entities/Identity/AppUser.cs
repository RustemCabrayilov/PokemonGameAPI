using Microsoft.AspNetCore.Identity;

namespace PokemonGameAPI.Domain.Entities.Identity;

public class AppUser:IdentityUser<Guid>
{
    public string LastIPAddress { get; set; }
}