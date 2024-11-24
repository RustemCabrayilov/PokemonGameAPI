using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Game;
using PokemonGameAPI.Application.Abstraction.Services.Gym;
using PokemonGameAPI.Application.Abstraction.Services.Pokemon;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Enums;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.Gym;

public class GymService(
    IGenericRepository<Domain.Entities.Gym> _gymRepository,
    IGenericRepository<Domain.Entities.Trainer> _trainerRepository,
    IGenericRepository<Domain.Entities.GymLeader> _gymLeaderRepository,
    IGenericRepository<Domain.Entities.Pokemon> _pokemonRepository,
    IGenericRepository<Domain.Entities.PokemonTrainer> _pokemonTrainerRepository,
    IGenericRepository<Domain.Entities.GymLeaderPokemon> _gymLeaderPokemonRepository,
    IGenericRepository<Domain.Entities.GymField> _gymFieldRepository,
    IGenericRepository<Domain.Entities.BossFightResult> _bossFightResultRepository,
    IMapper _mapper,
    IUnitOfWork _unitOfWork
  ) : IGymService
{
    private  List<Domain.Entities.Pokemon> _pokemons;
    public async Task<GymResponseDto> CreateAsync(GymRequestDto dto)
    {
        var entity = _mapper.Map<Domain.Entities.Gym>(dto);
        await _gymRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var gymLeader = await _gymLeaderRepository.GetAsync(entity.GymLeaderId);
        entity.GymLeader = gymLeader;
        var outDto = _mapper.Map<GymResponseDto>(entity);
        return outDto;
    }

    public async Task<GymResponseDto> UpdateAsync(Guid id, GymRequestDto dto)
    {
        var entity = await _gymRepository.GetAsync(id, "Trainers", "GymLeaders");
        if (entity is null) throw new NotFoundException("Gym not found");
        var entityToUpdate = _mapper.Map<Domain.Entities.Gym>(dto);
        entityToUpdate.Id = entity.Id;
        _gymRepository.Update(entityToUpdate);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<GymResponseDto>(entity);
        return outDto;
    }

    public async Task<GymResponseDto> RemoveAsync(Guid id)
    {
        var entity = await _gymRepository.GetAsync(id, "Trainers", "GymLeaders");
        if (entity is null) throw new NotFoundException("Gym not found");
        _gymRepository.Remove(entity);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<GymResponseDto>(entity);
        return outDto;
    }

    public async Task<GymResponseDto> GetAsync(Guid id)
    {
        var entity = await _gymRepository.GetAsync(id, "Trainers", "GymLeaders");
        if (entity is null) throw new NotFoundException("Gym not found");
        var outDto = _mapper.Map<GymResponseDto>(entity);
        return outDto;
    }

    public async Task<IList<GymResponseDto>> GetAllAsync()
    {
        var entities = await _gymRepository.GetAll().ToListAsync();
        var outDto = _mapper.Map<IList<GymResponseDto>>(entities);
        return outDto;
    }
    
    public async Task<GymResponseDto> StartBattleAsync(Guid mainTrainerId, Guid gymId)
    {
        var trainer = await _trainerRepository.GetAsync(mainTrainerId);
        if(trainer is null) throw new NotFoundException("Trainer not found");
        var gym = await _gymRepository.GetAsync(gymId);
        if(gym is null) throw new NotFoundException("Gym not found");
        var playerPokemon = await GetReadyPokemonForTrainer(mainTrainerId);
        if (playerPokemon is null)
            throw new NotFoundException($"No battle-ready Pokemon found for Trainer {mainTrainerId}");
        if (gym.GymLevel >= playerPokemon.PokemonLevel) throw new BadRequestException("Gym level is higher than your pokemon level");
        trainer.ReadyForGymBattle = true;
        _trainerRepository.Update(trainer);
        _unitOfWork.SaveChanges();
        var gymLeader = await _gymLeaderRepository.GetAsync(gym.GymLeaderId);
        gym.TrainerId = mainTrainerId;
        var gymField = new GymField()
        {
            Name = "Gym field" + new Random().Next(1, 10),
            Description = $"Fight with {gymLeader.Name} gym leader",
            GymId = gym.Id
        };
      
        int currentGymLeaderPokemonIndex = 0;
        _pokemons = GetReadyPokemonForGymLeader(gymLeader.Id);
        var gymLeaderPokemon = _pokemons[currentGymLeaderPokemonIndex];
        if (gymLeaderPokemon is null)
            throw new NotFoundException($"No battle-ready Pokemon found for Gym Leader {gymLeader.Id}");
        var bossFightResult = new BossFightResult
        {
            GymId = gym.Id,
            WinnerId = playerPokemon.HP > gymLeaderPokemon.HP ? mainTrainerId : gymLeader.Id,
            Winner = playerPokemon.HP > gymLeaderPokemon.HP ? ParticipantType.Trainer : ParticipantType.GymLeader,
            LooserId = playerPokemon.HP > gymLeaderPokemon.HP ? gymLeader.Id : mainTrainerId,
            Looser = playerPokemon.HP > gymLeaderPokemon.HP ? ParticipantType.GymLeader : ParticipantType.Trainer
        };
        await _bossFightResultRepository.AddAsync(bossFightResult);
        await _unitOfWork.SaveChangesAsync();
        _gymRepository.Update(gym);
        _unitOfWork.SaveChanges();
        _gymFieldRepository.AddAsync(gymField);
        _unitOfWork.SaveChanges();
        return _mapper.Map<GymResponseDto>(gym);
    }

    public async Task<BossFightResponseDto> BossFight(BossFightRequestDto requestDto)
    {
        var trainer = await _trainerRepository.GetAsync(requestDto.TrainerId);
        if (trainer is null) throw new NotFoundException("Trainer not found");
        var gym = await _gymRepository.GetAsync(requestDto.GymId);
        if (gym is null) throw new NotFoundException("Gym not found");
        var gymLeader = await _gymLeaderRepository.GetAsync(gym.GymLeaderId);
        if (gymLeader is null) throw new NotFoundException("Gym leader not found");
        var trainerPokemon = await GetReadyPokemonForTrainer(requestDto.TrainerId);
        if (trainerPokemon is null) throw new NotFoundException($"No battle-ready Pokémon found for Trainer {requestDto.TrainerId}");
        int currentGymLeaderPokemonIndex = 0;
        var gymLeaderPokemons=GetReadyPokemonForGymLeader(gymLeader.Id);
        var gymLeaderPokemon= gymLeaderPokemons[currentGymLeaderPokemonIndex];
        var bossFightResult =
            await _bossFightResultRepository.GetAll().FirstOrDefaultAsync(x => x.GymId == requestDto.GymId);
        if (bossFightResult is null) throw new NotFoundException("Boss fight not found");
        if (gymLeaderPokemon.HP <= 0)
        {
            currentGymLeaderPokemonIndex++;
            if (currentGymLeaderPokemonIndex >= gymLeaderPokemons.Count)
            {
                bossFightResult.Winner = ParticipantType.Trainer;
                bossFightResult.WinnerId = trainer.Id;
                bossFightResult.Looser = ParticipantType.GymLeader;
                bossFightResult.LooserId = gymLeader.Id;
                bossFightResult.WinnerDamages = requestDto.TrainerDamages;
                bossFightResult.LooserDamages = requestDto.GymLeaderDamages;
                trainerPokemon.PokemonLevel += new Random().Next(1,3);
                _pokemonRepository.Update(trainerPokemon);
                _unitOfWork.SaveChanges();
            }
        }
       else if (trainerPokemon.HP <= 0)
        {
            bossFightResult.Winner = ParticipantType.GymLeader;
            bossFightResult.WinnerId = gymLeader.Id;
            bossFightResult.Looser = ParticipantType.Trainer;
            bossFightResult.LooserId = trainer.Id;
            bossFightResult.WinnerDamages = requestDto.GymLeaderDamages;
            bossFightResult.LooserDamages = requestDto.TrainerDamages;
        }
        _bossFightResultRepository.Update(bossFightResult);
        _unitOfWork.SaveChanges();
        bossFightResult.Gym=await _gymRepository.GetAsync(bossFightResult.GymId);
        return _mapper.Map<BossFightResponseDto>(bossFightResult);
    }
    private async Task<Domain.Entities.Pokemon?> GetReadyPokemonForTrainer(Guid trainerId)
    {
        return await _pokemonRepository.GetAll()
            .Where(pokemon => _pokemonTrainerRepository.GetAll()
                .Any(tp => tp.TrainerId == trainerId && tp.PokemonId == pokemon.Id))
            .FirstOrDefaultAsync(x => x.ReadyForBattle);
    }

    private  List<Domain.Entities.Pokemon> GetReadyPokemonForGymLeader(Guid gymLeaderId)
    {
        return  _pokemonRepository.GetAll()
            .Where(pokemon => _gymLeaderPokemonRepository.GetAll()
                .Any(gp => gp.GymLeaderId == gymLeaderId && gp.PokemonId == pokemon.Id)).ToList();
    }
    public async ValueTask<bool> ResetAsync(ResetGymDto requestDto)
    {
     var pokemon1 = await _pokemonRepository.GetAsync(requestDto.Pokemon1Id);
     if (pokemon1 is null) throw new NotFoundException("Pokemon 1 not found");
     var pokemon2 = await _pokemonRepository.GetAsync(requestDto.Pokemon2Id);
     if (pokemon2 is null) throw new NotFoundException("Pokemon 2 not found");
        pokemon1.HP = pokemon1.PokemonLevel * 10 + (int)pokemon1.RarityType * 50;
        pokemon2.HP = pokemon2.PokemonLevel * 10 + (int)pokemon2.RarityType * 50;
        pokemon1.ReadyForBattle = false;
        pokemon2.ReadyForBattle = false;
        _pokemonRepository.Update(pokemon1);
        _pokemonRepository.Update(pokemon2);
        await _unitOfWork.SaveChangesAsync();
        var gymField = await _gymFieldRepository.GetAll().FirstOrDefaultAsync(x=>x.GymId==requestDto.GymId);
        _gymFieldRepository.Remove(gymField);
       await _unitOfWork.SaveChangesAsync();
        return true;
    }
    /*private async Task<Domain.Entities.Pokemon?> GetReadyPokemonForBoss(Guid gymLeaderId)
    {
        var readyPokemon=new Domain.Entities.Pokemon();
        var pokemons = await _pokemonRepository.GetAll().ToListAsync();
        var bossPokemons =
            await _gymLeaderPokemonRepository.GetAll().Where(pt => pt.GymLeaderId == gymLeaderId).ToListAsync();
        foreach (var pokemon in pokemons)
        {
            foreach (var bossPokemon in bossPokemons)
            {
                if (bossPokemon.PokemonId == pokemon.Id)
                {
                    readyPokemon = pokemon;
                    return readyPokemon;
                }
            }
        }
        return readyPokemon??null;
    }*/
    private int CalculateDamage(Domain.Entities.Pokemon attacker, Domain.Entities.Pokemon defender)
    {
        return Math.Max(0, attacker.HP - defender.AttitudeType == AttitudeType.Defense ? defender.HP : 0);
    }
}