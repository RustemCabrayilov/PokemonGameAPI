using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.EvaluationPokemon;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.EvaluationPokemonService;

public class EvolutionPokemonService(IGenericRepository<EvolutionPokemon> _evaluationPokemonRepository,
IUnitOfWork _unitOfWork,
IMapper _mapper,
IValidator<EvolutionPokemonRequestDto> _validator):IEvolutionPokemonService
{
    public async Task<EvolutionPokemonResponseDto> CreateAsync(EvolutionPokemonRequestDto dto)
    {
        var result =await _validator.ValidateAsync(dto);
        if (!result.IsValid) throw new ValidationException(result.Errors);
        var entity=_mapper.Map<EvolutionPokemon>(dto);
        await _evaluationPokemonRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var outDto=_mapper.Map<EvolutionPokemonResponseDto>(entity);
        return outDto;
    }

    public async Task<EvolutionPokemonResponseDto> UpdateAsync(Guid id, EvolutionPokemonRequestDto dto)
    {
        var result =await _validator.ValidateAsync(dto);
        if (!result.IsValid) throw new ValidationException(result.Errors);
        var entity = await _evaluationPokemonRepository.GetAsync(id);
        if (entity is null) throw new NotFoundException("Pokemon evaluation not found");
        _mapper.Map(dto,entity);
        _evaluationPokemonRepository.Update(entity);
        _unitOfWork.SaveChanges();
        var outDto=_mapper.Map<EvolutionPokemonResponseDto>(entity);
        return outDto;
    }

    public async Task<EvolutionPokemonResponseDto> RemoveAsync(Guid id)
    {
        var entity =await _evaluationPokemonRepository.GetAsync(id);
        if (entity is null) throw new NotFoundException("Pokemon evaluation not found");
        _evaluationPokemonRepository.Remove(entity);
        _unitOfWork.SaveChanges();
        var outDto=_mapper.Map<EvolutionPokemonResponseDto>(entity);
        return outDto;
    }

    public async Task<EvolutionPokemonResponseDto> GetAsync(Guid id)
    {
        var entity =await _evaluationPokemonRepository.GetAsync(id);
        if (entity is null) throw new NotFoundException("Pokemon evaluation not found");
        var outDto = _mapper.Map<EvolutionPokemonResponseDto>(entity);
        return outDto;
    }

    public async Task<IList<EvolutionPokemonResponseDto>> GetAllAsync()
    {
        var entities=await _evaluationPokemonRepository.GetAll().ToListAsync();
        var outDtos=_mapper.Map<IList<EvolutionPokemonResponseDto>>(entities);
        return outDtos;
    }
}