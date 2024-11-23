using AutoMapper;
using PokemonGameAPI.Domain.Entities.Identity;

namespace PokemonGameAPI.Application.Abstraction.Services.Role;

public class RoleMappingProfile: Profile
{
    public RoleMappingProfile()
    {
        CreateMap<RoleRequestDto, AppRole>();
        CreateMap<AppRole,RoleResponseDto>();
    }
}