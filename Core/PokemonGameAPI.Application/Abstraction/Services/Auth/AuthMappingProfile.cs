using AutoMapper;
using PokemonGameAPI.Domain.Entities.Identity;

namespace PokemonGameAPI.Application.Abstraction.Services.Auth;

public class AuthMappingProfile:Profile
{
    public AuthMappingProfile()
    {
        CreateMap<SignUpRequestDto, AppUser>();
    }
}