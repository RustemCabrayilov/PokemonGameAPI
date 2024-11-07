using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Game;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.Game;

public class GameService(IGenericRepository<Domain.Entities.Game> _gameRepository,
    IGenericRepository<Domain.Entities.Trainer> _trainerRepository,
    IMapper _mapper,
    IUnitOfWork _unitOfWork):IGameService
{
    public async Task<GameResponseDto> CreateAsync(GameRequestDto dto)
    {
        var entity =_mapper.Map<Domain.Entities.Game>(dto);
        await _gameRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var trainer1 = await _trainerRepository.GetAsync(entity.Trainer1Id);
        var trainer2 = await _trainerRepository.GetAsync(entity.Trainer2Id);
        entity.Trainer1 = trainer1;
        entity.Trainer2 = trainer2;
        var outDto=_mapper.Map<GameResponseDto>(entity);
        return outDto;
    }

    public async Task<GameResponseDto> UpdateAsync(Guid id, GameRequestDto dto)
    {
      var entity = await _gameRepository.GetAsync(id,"Trainer1","Trainer2");
      if (entity is null) throw new NotFoundException("Game not found");
      var entityToUpdate = _mapper.Map<Domain.Entities.Game>(dto);
      entityToUpdate.Id=entity.Id;
      _gameRepository.Update(entityToUpdate);
      _unitOfWork.SaveChanges();
      var outDto=_mapper.Map<GameResponseDto>(entity);
      return outDto;
    }

    public async Task<GameResponseDto> RemoveAsync(Guid id)
    {
        var entity = await _gameRepository.GetAsync(id,"Trainer1","Trainer2");
        if (entity is null) throw new NotFoundException("Game not found");
        _gameRepository.Remove(entity);
        _unitOfWork.SaveChanges();
        var outDto=_mapper.Map<GameResponseDto>(entity);
        return outDto;
    }

    public async Task<GameResponseDto> GetAsync(Guid id)
    {
        var entity = await _gameRepository.GetAsync(id,"Trainer1","Trainer2");
        if (entity is null) throw new NotFoundException("Game not found");
        var outDto=_mapper.Map<GameResponseDto>(entity);
        return outDto;
    }

    public async Task<IList<GameResponseDto>> GetAllAsync()
    {
        var entities = await _gameRepository.GetAll().ToListAsync();
        var outDtos = _mapper.Map<IList<GameResponseDto>>(entities);
        return outDtos;
    }
}