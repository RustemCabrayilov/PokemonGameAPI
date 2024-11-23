using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokemonGameAPI.Application.Abstraction.Services.User;
using PokemonGameAPI.Domain.Entities.Identity;
using PokemonGameAPI.Infrastructure.Exceptions;
using Serilog;

namespace PokemonGameAPI.Infrastructure.Services.User;

public class UserService(
    UserManager<AppUser> _userManager,
    RoleManager<AppRole> _roleManager,
    IMapper _mapper,
    ILogger<UserService> _logger) : IUserService
{
    public async Task<UserResponseDto> CreateAsync(UserRequestDto dto)
    {
        var entity = _mapper.Map<AppUser>(dto);
        entity.LastIPAddress = "0.0.0.0";/*_httpContext.Connection.RemoteIpAddress?.ToString();*/
        entity.Id = Guid.NewGuid().ToString();
        var result = await _userManager.CreateAsync(entity, dto.Password);
        if (!result.Succeeded) throw new Exception("Failed to create user");
        var outDto = _mapper.Map<UserResponseDto>(entity);
        var dtoOutputToJson = JsonSerializer.Serialize(outDto);
        _logger.LogInformation(dtoOutputToJson);
        return outDto;
    }

    public async Task<UserResponseDto> UpdateAsync(string id, UserRequestDto dto)
    {
        var entity = await _userManager.FindByIdAsync(id);
        if (entity is null) throw new NotFoundException("User not found");
        _mapper.Map(dto, entity);
        await _userManager.RemovePasswordAsync(entity);
        await _userManager.AddPasswordAsync(entity, dto.Password);
        var result = await _userManager.UpdateAsync(entity);
        if (!result.Succeeded) throw new Exception("Failed to update user");
        var outDto = _mapper.Map<UserResponseDto>(entity);
        var userRoles = await _userManager.GetRolesAsync(entity);
        outDto.Roles.AddRange(userRoles);
        var dtoOutputToJson = JsonSerializer.Serialize(outDto);
        _logger.LogInformation(dtoOutputToJson);
        return outDto;
    }

    public async Task<UserResponseDto> RemoveAsync(string id)
    {
        var entity = await _userManager.FindByIdAsync(id);
        if (entity is null) throw new NotFoundException("User not found");
        var userRoles = await _userManager.GetRolesAsync(entity);
        var result = await _userManager.DeleteAsync(entity);
        if (!result.Succeeded) throw new Exception("Failed to remove user");
        var outDto = _mapper.Map<UserResponseDto>(entity);
        outDto.Roles.AddRange(userRoles);
        var dtoOutputToJson = JsonSerializer.Serialize(outDto);
        _logger.LogInformation(dtoOutputToJson);
        return outDto;
    }

    public async Task<UserResponseDto> GetAsync(string id)
    {
        var entity = await _userManager.FindByIdAsync(id);
        if (entity is null) throw new NotFoundException("User not found");
        var outDto = _mapper.Map<UserResponseDto>(entity);
        var userRoles = await _userManager.GetRolesAsync(entity);
        outDto.Roles.AddRange(userRoles);
        var dtoOutputToJson = JsonSerializer.Serialize(outDto);
        _logger.LogInformation(dtoOutputToJson);
        return outDto;
    }

    public async Task<IList<UserResponseDto>> GetAllAsync()
    {
        var entities = await _userManager.Users.ToListAsync();
        var outDtos = _mapper.Map<List<UserResponseDto>>(entities);
        foreach (var outDto in outDtos)
        {
            foreach (var entity in entities)
            {
                var userRoles = await _userManager.GetRolesAsync(entity);
                outDto.Roles.AddRange(userRoles);
            }
        }
        var dtoOutputToJson = JsonSerializer.Serialize(outDtos);
        _logger.LogInformation(dtoOutputToJson);
        return outDtos;
    }

    public async Task<UserResponseDto> AssignRoleAsync(UserRoleDto dto)
    {
        var entity = await _userManager.FindByIdAsync(dto.UserId);
        if(entity is null) throw new NotFoundException("User not found");
        var role=await _roleManager.FindByNameAsync(dto.RoleName);
        if(role is null) throw new NotFoundException("Role not found");
       var result = await _userManager.AddToRoleAsync(entity, dto.RoleName);
       if(!result.Succeeded) throw new Exception("Failed to assign role");
        var outDto = _mapper.Map<UserResponseDto>(entity);
        var userRoles = await _userManager.GetRolesAsync(entity);
        outDto.Roles.AddRange(userRoles);
        var dtoOutputToJson = JsonSerializer.Serialize(outDto);
        _logger.LogInformation(dtoOutputToJson);
        return outDto;
    }
}