using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokemonGameAPI.Application.Abstraction.Services.Role;
using PokemonGameAPI.Domain.Entities.Identity;
using PokemonGameAPI.Infrastructure.Exceptions;
using Serilog;

namespace PokemonGameAPI.Infrastructure.Services.Role;

public class RoleService(RoleManager<AppRole> _roleManager,
    IMapper _mapper,
    ILogger<RoleService> _logger):IRoleService
{
    public async Task<RoleResponseDto> CreateAsync(RoleRequestDto dto)
    {
     var entity = _mapper.Map<AppRole>(dto);   
     entity.Id = Guid.NewGuid().ToString();
     await _roleManager.CreateAsync(entity);
     var outDto = _mapper.Map<RoleResponseDto>(entity);
     var dtoOutputToJson = JsonSerializer.Serialize(outDto);
     _logger.LogInformation(dtoOutputToJson);
     return outDto;
    }

    public async Task<RoleResponseDto> UpdateAsync(string id, RoleRequestDto dto)
    {
     var entity = await _roleManager.FindByIdAsync(id);
    if(entity is null) throw new NotFoundException("Role not found");
    var entityToUpdate = _mapper.Map<AppRole>(dto);
    entityToUpdate.Id=entity.Id;
    await _roleManager.UpdateAsync(entityToUpdate);
    var outDto = _mapper.Map<RoleResponseDto>(entity);
    var dtoOutputToJson = JsonSerializer.Serialize(outDto);
    _logger.LogInformation(dtoOutputToJson);
    return outDto;
    }

    public async Task<RoleResponseDto> RemoveAsync(string id)
    {
        var entity = await _roleManager.FindByIdAsync(id);
        if(entity is null) throw new NotFoundException("Role not found");
        await _roleManager.DeleteAsync(entity);
        var outDto = _mapper.Map<RoleResponseDto>(entity);
        var dtoOutputToJson = JsonSerializer.Serialize(outDto);
        _logger.LogInformation(dtoOutputToJson);
        return outDto;
    }

    public async Task<RoleResponseDto> GetAsync(string id)
    {
        var entity = await _roleManager.FindByIdAsync(id);
        if(entity is null) throw new NotFoundException("Role not found");
        var outDto = _mapper.Map<RoleResponseDto>(entity);
        var dtoOutputToJson = JsonSerializer.Serialize(outDto);
        _logger.LogInformation(dtoOutputToJson);
        return outDto;
    }

    public async Task<IList<RoleResponseDto>> GetAllAsync()
    {
        var entities =  await _roleManager.Roles.ToListAsync();
        var outDtos = _mapper.Map<IList<RoleResponseDto>>(entities);
        var dtoOutputToJson = JsonSerializer.Serialize(outDtos);
        _logger.LogInformation(dtoOutputToJson);
        return outDtos;
    }
}