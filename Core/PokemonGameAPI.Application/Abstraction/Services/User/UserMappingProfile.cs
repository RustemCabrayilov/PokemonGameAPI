using AutoMapper;
using PokemonGameAPI.Domain.Entities.Identity;

namespace PokemonGameAPI.Application.Abstraction.Services.User;

public class UserMappingProfile:Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserRequestDto, AppUser>();
        CreateMap<AppUser,UserResponseDto>();
    }
}