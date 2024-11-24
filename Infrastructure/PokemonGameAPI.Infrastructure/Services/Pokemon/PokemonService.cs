using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Pokemon;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Enums;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.Pokemon;

public class PokemonService(
    IGenericRepository<Domain.Entities.Pokemon> _pokemonRepository,
    IGenericRepository<Domain.Entities.Skill> _skillRepository,
    IGenericRepository<Domain.Entities.Category> _categoryRepository,
    IGenericRepository<Domain.Entities.EvolutionPokemon> _evaluationPokemonRepository,
    IMapper _mapper,
    IUnitOfWork _unitOfWork) : IPokemonService
{
    public async Task<PokemonResponseDto> CreateAsync(PokemonRequestDto dto)
    {
        var entity = _mapper.Map<Domain.Entities.Pokemon>(dto);
        entity.HP = entity.PokemonLevel * 10 + (int)entity.RarityType * 50;
        entity.Attack = entity.PokemonLevel * 10 + (int)entity.RarityType * 5;
        entity.Defense = entity.PokemonLevel * 5 + (int)entity.RarityType * 5;
        await _pokemonRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var skill = await _skillRepository.GetAsync(entity.SkillId);
        var category = await _categoryRepository.GetAsync(entity.CategoryId);
        entity.Skill = skill;
        entity.Category = category;
        var outDto = _mapper.Map<PokemonResponseDto>(entity);
        return outDto;
    }

    public async Task<PokemonResponseDto> UpdateAsync(Guid id, PokemonRequestDto dto)
    {
        var entity = await _pokemonRepository.GetAsync(id, "Skills", "Categories");
        if (entity == null)
        {
            throw new NotFoundException($"This {id} pokemon does not exist");
        }

        _mapper.Map(dto, entity);
        entity.HP = entity.PokemonLevel * 10 + (int)entity.RarityType * 50;
        entity.Attack = entity.PokemonLevel * 10 + (int)entity.RarityType * 5;
        entity.Defense = entity.PokemonLevel * 5 + (int)entity.RarityType * 5;
        _pokemonRepository.Update(entity);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<PokemonResponseDto>(entity);
        return outDto;
    }

    public async Task<PokemonResponseDto> RemoveAsync(Guid id)
    {
        var entity = await _pokemonRepository.GetAsync(id, "Skills", "Categories");
        if (entity == null)
        {
            throw new NotFoundException($"This {id} pokemon does not exist");
        }

        _pokemonRepository.Remove(entity);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<PokemonResponseDto>(entity);
        return outDto;
    }

    public async Task<PokemonResponseDto> GetAsync(Guid id)
    {
        var entity = await _pokemonRepository.GetAsync(id, "Skills", "Categories");
        if (entity == null)
        {
            throw new NotFoundException($"This {id} pokemon does not exist");
        }

        var outDto = _mapper.Map<PokemonResponseDto>(entity);
        return outDto;
    }

    public async Task<IList<PokemonResponseDto>> GetAllAsync()
    {
        var entities = await _pokemonRepository.GetAll().ToListAsync();
        var outDtos = _mapper.Map<IList<PokemonResponseDto>>(entities);
        return outDtos;
    }

    public async Task<PokemonResponseDto> Attack(Guid opponentId, int damage)
    {
        var opponent = await _pokemonRepository.GetAsync(opponentId);
        if (opponent is null) throw new NotFoundException($"This {opponentId} pokemon does not exist");
        TakeDamage(opponent, damage);
        var outDto = _mapper.Map<PokemonResponseDto>(opponent);
        return outDto;
    }

    private void TakeDamage(Domain.Entities.Pokemon opponent, int damage)
    {
        opponent.HP -= CalculateDamage(opponent, damage);
        _pokemonRepository.Update(opponent);
        _unitOfWork.SaveChanges();
    }

    private int CalculateDamage(Domain.Entities.Pokemon opponent, int damage)
    {
        return Math.Max(0, damage - opponent.AttitudeType == AttitudeType.Defense ? opponent.Defense : damage);
    }

    public async Task<PokemonResponseDto> Defense(Guid defenderId)
    {
        var defender = await _pokemonRepository.GetAsync(defenderId);
        if (defender is null) throw new NotFoundException($"This {defenderId} pokemon does not exist");
        defender.AttitudeType = AttitudeType.Defense;
        _pokemonRepository.Update(defender);
        await _unitOfWork.SaveChangesAsync();
        var outDto = _mapper.Map<PokemonResponseDto>(defender);
        return outDto;
    }

    public async Task<PokemonResponseDto> SetPokemonReady(Guid id)
    {
        var pokemon = await _pokemonRepository.GetAsync(id);
        pokemon.ReadyForBattle = true;
        _pokemonRepository.Update(pokemon);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<PokemonResponseDto>(pokemon);
        return outDto;
    }

    public async Task<PokemonResponseDto> CheckEvaluationAsync(Guid id)
    {
        var entity = await _pokemonRepository.GetAsync(id);
        if (entity is null) throw new NotFoundException($"This {id} pokemon does not exist");
        if (entity.PokemonLevel == entity.EvolutionLevel)
        {
            entity.IsEvolution = true;
            _pokemonRepository.Update(entity);
            _unitOfWork.SaveChanges();
        }
        var outDto = _mapper.Map<PokemonResponseDto>(entity);
        return outDto;   
    }
    public async Task<PokemonResponseDto> EvolutionUpdateAsync(Guid id)
    {
        var entity = await _pokemonRepository.GetAsync(id);
        if (entity is null) throw new NotFoundException($"This {id} pokemon does not exist");
        var evaluationPokemon = await _evaluationPokemonRepository.GetAll()
            .FirstOrDefaultAsync(x => x.PokemonId == entity.Id && x.EvolutionLevel == entity.EvolutionLevel);
        if (evaluationPokemon is null) throw new NotFoundException($"This {id} pokemon could not have evalutation");
        _mapper.Map(evaluationPokemon, entity);
        entity.EvolutionLevel = evaluationPokemon.EvolutionLevel*2;
        _pokemonRepository.Update(entity);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<PokemonResponseDto>(entity);
        return outDto;
    }
}