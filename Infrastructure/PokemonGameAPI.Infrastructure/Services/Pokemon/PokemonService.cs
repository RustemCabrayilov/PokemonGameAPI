using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Pokemon;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.Pokemon;

public class PokemonService(
    IGenericRepository<Domain.Entities.Pokemon> _pokemonRepository,
    IGenericRepository<Domain.Entities.Skill> _skillRepository,
    IGenericRepository<Domain.Entities.Category> _categoryRepository,
    IMapper _mapper,
    IUnitOfWork _unitOfWork) : IPokemonService
{
    public async Task<PokemonResponseDto> CreateAsync(PokemonRequestDto dto)
    {
        var entity = _mapper.Map<Domain.Entities.Pokemon>(dto);
        await _pokemonRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var skill = await _skillRepository.GetAsync(entity.SkillId);
        var category =await _categoryRepository.GetAsync(entity.CategoryId);
        entity.Skill = skill;
        entity.Category = category;
        var outDto = _mapper.Map<PokemonResponseDto>(entity);
        return outDto;
    }
    
    public async Task<PokemonResponseDto> UpdateAsync(Guid id,PokemonRequestDto dto)
    {
        var entity = await _pokemonRepository.GetAsync(id,"Skills","Categories");
        if (entity == null)
        {
            throw new NotFoundException($"This {id} pokemon does not exist");
        }
        var entityToUpdate = _mapper.Map<Domain.Entities.Pokemon>(dto);
        entityToUpdate.Id = entity.Id;
        _pokemonRepository.Update(entity);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<PokemonResponseDto>(entityToUpdate);
        return outDto;
    }

    public async Task<PokemonResponseDto> RemoveAsync(Guid id)
    {
        var entity = await _pokemonRepository.GetAsync(id,"Skills","Categories");
        if (entity == null)
        {
            throw new NotFoundException($"This {id} pokemon does not exist");
        }
        var outDto = _mapper.Map<PokemonResponseDto>(entity);
        return outDto;
    }

    public async Task<PokemonResponseDto> GetAsync(Guid id)
    {
      var entity = await _pokemonRepository.GetAsync(id,"Skills","Categories");
      if (entity == null)
      {
          throw new NotFoundException($"This {id} pokemon does not exist");
      }
      var outDto = _mapper.Map<PokemonResponseDto>(entity);
      return outDto;
    }

    public async Task<IList<PokemonResponseDto>> GetAllAsync()
    {
       var entities =await _pokemonRepository.GetAll().ToListAsync();
       var outDtos = _mapper.Map<IList<PokemonResponseDto>>(entities);
       return outDtos;
    }
}