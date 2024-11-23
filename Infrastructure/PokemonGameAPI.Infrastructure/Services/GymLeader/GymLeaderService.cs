using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.GymLeader;
using PokemonGameAPI.Application.Abstraction.Services.Pokemon;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.GymLeader;

public class GymLeaderService(
    IGenericRepository<Domain.Entities.GymLeader> _gymLeaderRepository,
    IGenericRepository<Domain.Entities.Gym> _gymRepository,
    IGenericRepository<Domain.Entities.Pokemon> _pokemonRepository,
    IGenericRepository<Domain.Entities.GymLeaderPokemon> _gymLeaderPokemonRepository,
    IMapper _mapper,
    IUnitOfWork _unitOfWork) : IGymLeaderService
{
    public async Task<GymLeaderResponseDto> CreateAsync(GymLeaderRequestDto dto)
    {
        var entity = _mapper.Map<Domain.Entities.GymLeader>(dto);
        await _gymLeaderRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var gyms = await _gymRepository.GetAll().Where(x=>x.GymLeaderId==entity.Id).ToListAsync();
        entity.Gyms =gyms;
        var gymLeader = await _gymLeaderRepository.GetAsync(entity.Id,"Gyms");
        var pokemonList =await GetPokemonsForGymLeader(entity.Id);
        var outDto = _mapper.Map<GymLeaderResponseDto>(entity);
        outDto.Pokemons.AddRange(_mapper.Map<List<PokemonResponseDto>>(pokemonList));;
        return outDto;
    }

    public async Task<GymLeaderResponseDto> UpdateAsync(Guid id, GymLeaderRequestDto dto)
    {
        var entity = await _gymLeaderRepository.GetAsync(id, "Gyms");
        if (entity is null) throw new NotFoundException("Gym leader not found");
        var entityToUpdate = _mapper.Map<Domain.Entities.GymLeader>(dto);
        _mapper.Map(dto,entity);
        _gymLeaderRepository.Update(entity);
        _unitOfWork.SaveChanges();
        var pokemonList = GetPokemonsForGymLeader(entity.Id);
        var outDto = _mapper.Map<GymLeaderResponseDto>(entity);
        outDto.Pokemons.AddRange(_mapper.Map<List<PokemonResponseDto>>(pokemonList));;
        return outDto;
    }

    public async Task<GymLeaderResponseDto> RemoveAsync(Guid id)
    {
        var entity = await _gymLeaderRepository.GetAsync(id, "Gyms");
        if (entity is null) throw new NotFoundException("Gym leader not found");
        _gymLeaderRepository.Remove(entity);
        _unitOfWork.SaveChanges();
        var pokemonList =await GetPokemonsForGymLeader(entity.Id);
        var outDto = _mapper.Map<GymLeaderResponseDto>(entity);
        outDto.Pokemons.AddRange(_mapper.Map<List<PokemonResponseDto>>(pokemonList));;
        return outDto;
    }

    public async Task<GymLeaderResponseDto> GetAsync(Guid id)
    {
        var entity = await _gymLeaderRepository.GetAsync(id, "Gyms");
        if (entity is null) throw new NotFoundException("Gym leader not found");
        var pokemonList =await GetPokemonsForGymLeader(entity.Id);
        var outDto = _mapper.Map<GymLeaderResponseDto>(entity);
        outDto.Pokemons.AddRange(_mapper.Map<List<PokemonResponseDto>>(pokemonList));;
        return outDto;
    }

    public async Task<IList<GymLeaderResponseDto>> GetAllAsync()
    {
        var entities = await _gymLeaderRepository.GetAll().ToListAsync();
        var outDtos = _mapper.Map<List<GymLeaderResponseDto>>(entities);
        return outDtos;
    }

    public async Task<GymLeaderResponseDto> AsssignPokemonAsync(GymLeaderPokemonDto dto)
    {
        var entity = _mapper.Map<GymLeaderPokemon>(dto);
        await _gymLeaderPokemonRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var gymLeader = await _gymLeaderRepository.GetAsync(dto.GymleaderId,"Gyms");
        var pokemonList = await GetPokemonsForGymLeader(gymLeader.Id);
        var outDto=_mapper.Map<GymLeaderResponseDto>(gymLeader);
        outDto.Pokemons.AddRange(_mapper.Map<List<PokemonResponseDto>>(pokemonList));;
        return outDto;
    }
    private async Task<List<Domain.Entities.Pokemon>> GetPokemonsForGymLeader(Guid gymLeaderId)
    {
        return await _pokemonRepository.GetAll()
            .Where(pokemon => _gymLeaderPokemonRepository.GetAll()
                .Any(gp => gp.GymLeaderId == gymLeaderId && gp.PokemonId == pokemon.Id)).ToListAsync();
    }
}