using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Game;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Enums;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.Game;

public class GameService(
    IGenericRepository<Domain.Entities.Game> _gameRepository,
    IGenericRepository<Domain.Entities.Arena> _arenaRepository,
    IGenericRepository<Domain.Entities.Trainer> _trainerRepository,
    IGenericRepository<Domain.Entities.Pokemon> _pokemonRepository,
    IGenericRepository<Domain.Entities.PokemonTrainer> _pokemonTrainerRepository,
    IGenericRepository<Domain.Entities.BattleResult> _battleResultRepository,
    IMapper _mapper,
    IUnitOfWork _unitOfWork) : IGameService
{
    public async Task<GameResponseDto> CreateAsync(GameRequestDto dto)
    {
        var entity = _mapper.Map<Domain.Entities.Game>(dto);
        await _gameRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var winnner = await _trainerRepository.GetAsync(entity.Trainer1Id);
        var looser = await _trainerRepository.GetAsync(entity.Trainer2Id);
        entity.Trainer1 = winnner;
        entity.Trainer2 = looser;
        var outDto = _mapper.Map<GameResponseDto>(entity);
        return outDto;
    }

    public async Task<GameResponseDto> UpdateAsync(Guid id, GameRequestDto dto)
    {
        var entity = await _gameRepository.GetAsync(id, "Trainer1", "Trainer2");
        if (entity is null) throw new NotFoundException("Game not found");
        var entityToUpdate = _mapper.Map<Domain.Entities.Game>(dto);
        entityToUpdate.Id = entity.Id;
        _gameRepository.Update(entityToUpdate);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<GameResponseDto>(entity);
        return outDto;
    }

    public async Task<GameResponseDto> RemoveAsync(Guid id)
    {
        var entity = await _gameRepository.GetAsync(id, "Trainer1", "Trainer2");
        if (entity is null) throw new NotFoundException("Game not found");
        _gameRepository.Remove(entity);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<GameResponseDto>(entity);
        return outDto;
    }

    public async Task<GameResponseDto> GetAsync(Guid id)
    {
        var entity = await _gameRepository.GetAsync(id, "Trainer1", "Trainer2");
        if (entity is null) throw new NotFoundException("Game not found");
        var outDto = _mapper.Map<GameResponseDto>(entity);
        return outDto;
    }

    public async Task<IList<GameResponseDto>> GetAllAsync()
    {
        var entities = await _gameRepository.GetAll().ToListAsync();
        var outDtos = _mapper.Map<IList<GameResponseDto>>(entities);
        return outDtos;
    }

    public async Task<GameResponseDto> StartBattleAsync(Guid trainer1Id, Guid trainer2Id)
    {
        var trainer1 = await _trainerRepository.GetAsync(trainer1Id);
        if (trainer1 is null) throw new NotFoundException($"This {trainer1Id} trainer does not exist");
        var trainer2 = await _trainerRepository.GetAsync(trainer2Id);
        if (trainer2 is null) throw new NotFoundException($"This {trainer2Id} trainer does not exist");
        var pokemon1 = await GetReadyPokemonForTrainer(trainer1Id);
        if (pokemon1 is null) throw new NotFoundException($"No battle-ready Pokémon found for Trainer {trainer1Id}");
        var pokemon2 = await GetReadyPokemonForTrainer(trainer2Id);
        if (pokemon2 is null) throw new NotFoundException($"No battle-ready Pokémon found for Trainer {trainer2Id}");
        _pokemonRepository.Update(pokemon1);
        _pokemonRepository.Update(pokemon2);
        await _unitOfWork.SaveChangesAsync();
        var game = new Domain.Entities.Game()
        {
            Trainer1 = trainer1,
            Trainer2 = trainer2,
            Trainer1Id = trainer1Id,
            Trainer2Id = trainer2Id
        };
        await _gameRepository.AddAsync(game);
        await _unitOfWork.SaveChangesAsync();
        var arena = new Domain.Entities.Arena();
        var battleResult = new Domain.Entities.BattleResult();
        arena.Name = $"Arena{new Random().Next(1,10)}";
        arena.GameId = game.Id;
        arena.Game = game;
        await _arenaRepository.AddAsync(arena);
        await _unitOfWork.SaveChangesAsync();
        battleResult.GameId = game.Id;
        battleResult.WinnerId=pokemon1.HP>pokemon2.HP?trainer1Id:trainer2Id;
        battleResult.LooserId=battleResult.WinnerId==trainer1Id?trainer2Id:trainer1Id;
        await _battleResultRepository.AddAsync(battleResult);
        await _unitOfWork.SaveChangesAsync();
        
        var outDto = _mapper.Map<GameResponseDto>(game);
        return outDto;
    }

    public async Task<BattleResultResponseDto> FightAsync(BattleResultRequestDto requestDto)
    {
        var pokemon1 = await GetReadyPokemonForTrainer(requestDto.Trainer1Id);
        var pokemon2 = await GetReadyPokemonForTrainer(requestDto.Trainer2Id);
        var battleResult = await _battleResultRepository.GetAll().FirstOrDefaultAsync(x => x.GameId == requestDto.GameId);
        if (pokemon1.HP <= 0)
        {
            battleResult.WinnerId = requestDto.Trainer2Id;
            battleResult.LooserId = requestDto.Trainer1Id;
            pokemon1.PokemonLevel += new Random().Next(1,3);
            battleResult.WinnerDamages = requestDto.Trainer2Damages;
            battleResult.LooserDamages = requestDto.Trainer1Damages;
            _pokemonRepository.Update(pokemon2);
            _unitOfWork.SaveChanges();
        }
        else if (pokemon2.HP<=0)
        {
            battleResult.WinnerId = requestDto.Trainer1Id;
            battleResult.LooserId = requestDto.Trainer2Id;
            pokemon1.PokemonLevel += new Random().Next(1,3);
            battleResult.WinnerDamages = requestDto.Trainer2Damages;
            battleResult.LooserDamages = requestDto.Trainer1Damages;
            _pokemonRepository.Update(pokemon1);
            _unitOfWork.SaveChanges();
        }
        battleResult.Winner=await _trainerRepository.GetAsync(battleResult.WinnerId);
        battleResult.Looser=await _trainerRepository.GetAsync(battleResult.LooserId);
        _battleResultRepository.Update(battleResult);
        _unitOfWork.SaveChanges();
        return _mapper.Map<BattleResultResponseDto>(battleResult);
    }

    private async Task<Domain.Entities.Pokemon?> GetReadyPokemonForTrainer(Guid trainerId)
    {
        return await _pokemonRepository.GetAll()
            .Where(pokemon => _pokemonTrainerRepository.GetAll()
                .Any(tp => tp.TrainerId == trainerId && tp.PokemonId == pokemon.Id))
            .FirstOrDefaultAsync(x=>x.ReadyForBattle);
    }

    public async ValueTask<bool> ResetAsync(ResetGameDto requestDto)
    {
        var pokemon1 = await _pokemonRepository.GetAsync(requestDto.Pokemon1Id);
        if (pokemon1 is null) throw new NotFoundException("Pokemon 1 not found");
        var pokemon2 = await _pokemonRepository.GetAsync(requestDto.Pokemon2Id);
        if (pokemon1 is null) throw new NotFoundException("Pokemon 2 not found");
        pokemon1.HP = pokemon1.PokemonLevel * 10 + (int)pokemon1.RarityType * 50;
        pokemon2.HP = pokemon2.PokemonLevel * 10 + (int)pokemon2.RarityType * 50;
        pokemon1.ReadyForBattle = false;
        pokemon2.ReadyForBattle = false;
        _pokemonRepository.Update(pokemon1);
        _pokemonRepository.Update(pokemon2);
        await _unitOfWork.SaveChangesAsync();
        var arena = await _arenaRepository.GetAll().FirstOrDefaultAsync(x=>x.GameId==requestDto.GameId);
        _arenaRepository.Remove(arena);
        return true;
    }
}