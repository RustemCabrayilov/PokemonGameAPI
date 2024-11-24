using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Badge;
using PokemonGameAPI.Application.Abstraction.Services.Pokemon;
using PokemonGameAPI.Application.Abstraction.Services.Quest;
using PokemonGameAPI.Application.Abstraction.Services.Trainer;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Entities.Identity;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.Trainer;

public class TrainerService(
    IGenericRepository<Domain.Entities.Trainer> _trainerRepository,
    IGenericRepository<Domain.Entities.Pokemon> _pokemonRepository,
    IGenericRepository<Domain.Entities.Badge> _badgeRepository,
    IGenericRepository<Domain.Entities.PokemonTrainer> _pokemonTrainerRepository,
    IGenericRepository<Domain.Entities.BadgeTrainer> _badgeTrainerRepository,
    IGenericRepository<Domain.Entities.QuestTrainer> _questTrainerRepository,
    IGenericRepository<Domain.Entities.Quest> _questRepository,
    UserManager<AppUser> _userManager,
    IMapper _mapper,
    IUnitOfWork _unitOfWork): ITrainerService
{
    public async Task<TrainerResponseDto> CreateAsync(TrainerRequestDto dto)
    {
        var entity=_mapper.Map<Domain.Entities.Trainer>(dto);
       await _trainerRepository.AddAsync(entity);
       await _unitOfWork.SaveChangesAsync();
        var outDto =_mapper.Map<TrainerResponseDto>(entity);
        return outDto;
    }

    public async Task<TrainerResponseDto> UpdateAsync(Guid id, TrainerRequestDto dto)
    {
        var entity = await _trainerRepository.GetAsync(id,"User");
        if(entity is null) throw new NotFoundException("Trainer not found");
        _mapper.Map(dto,entity);
        _trainerRepository.Update(entity);
        _unitOfWork.SaveChanges();
        var pokemonList = GetPokemonsForTrainer(entity.Id);
        var badgeList = GetBadgesForTrainer(entity.Id);
        var outDto = _mapper.Map<TrainerResponseDto>(entity);
        outDto.Pokemons.AddRange(_mapper.Map<List<PokemonResponseDto>>(pokemonList));; 
        outDto.Badges.AddRange(_mapper.Map<List<BadgeResponseDto>>(badgeList));;

        return outDto;
    }

    public async Task<TrainerResponseDto> RemoveAsync(Guid id)
    {
        var entity = await _trainerRepository.GetAsync(id,"User");
        if(entity is null) throw new NotFoundException("Trainer Not Found");
        _trainerRepository.Remove(entity);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<TrainerResponseDto>(entity);
        var pokemonList = GetPokemonsForTrainer(entity.Id);
        var badgeList = GetBadgesForTrainer(entity.Id);
        outDto.Pokemons.AddRange(_mapper.Map<List<PokemonResponseDto>>(pokemonList));;
        outDto.Badges.AddRange(_mapper.Map<List<BadgeResponseDto>>(badgeList));;
        return outDto;
    }

    public async Task<TrainerResponseDto> GetAsync(Guid id)
    {
        var entity = await _trainerRepository.GetAsync(id,"User");
        if(entity is null) throw new NotFoundException("Trainer Not Found");
        var pokemonList = await GetPokemonsForTrainer(entity.Id);
        var badgeList = await GetBadgesForTrainer(entity.Id);
        var outDto=_mapper.Map<TrainerResponseDto>(entity);
        outDto.Pokemons.AddRange(_mapper.Map<List<PokemonResponseDto>>(pokemonList));;
        outDto.Badges.AddRange(_mapper.Map<List<BadgeResponseDto>>(badgeList));;
        return outDto;
    }

    public async Task<IList<TrainerResponseDto>> GetAllAsync()
    {
       var entities=await _trainerRepository.GetAll().ToListAsync();
       var outDtos = _mapper.Map<List<TrainerResponseDto>>(entities);
       return outDtos;
    }

    public async Task<TrainerResponseDto> AssignPokemonAsync(TrainerPokemonDto dto)
    {
        var entity = _mapper.Map<PokemonTrainer>(dto);
        if(entity is null) throw new BadRequestException("Pokemon couldn't be assigned to trainer");
        await _pokemonTrainerRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var trainer = await _trainerRepository.GetAsync(dto.TrainerId,"User");
        var pokemonList = await GetPokemonsForTrainer(trainer.Id);
        var outDto=_mapper.Map<TrainerResponseDto>(trainer);
        outDto.Pokemons.AddRange(_mapper.Map<List<PokemonResponseDto>>(pokemonList));;
        return outDto;
    }

    public async Task<TrainerResponseDto> AsssignBadgeAsync(TrainerBadgeDto dto)
    {
        var entity = _mapper.Map<BadgeTrainer>(dto);
        if(entity is null) throw new BadRequestException("Badge couldn't be assigned to trainer");
        await _badgeTrainerRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var trainer = await _trainerRepository.GetAsync(dto.TrainerId,"User");
        var badgeList = await GetBadgesForTrainer(trainer.Id);
        var outDto=_mapper.Map<TrainerResponseDto>(entity);
        outDto.Badges.AddRange(_mapper.Map<List<BadgeResponseDto>>(badgeList));;
        return outDto;
    }
    private async Task<List<Domain.Entities.Pokemon>> GetPokemonsForTrainer(Guid trainerId)
    {
        return await _pokemonRepository.GetAll()
            .Where(pokemon => _pokemonTrainerRepository.GetAll()
                .Any(tp => tp.TrainerId == trainerId && tp.PokemonId == pokemon.Id)).ToListAsync();
    }
    private async Task<List<Domain.Entities.Badge>> GetBadgesForTrainer(Guid trainerId)
    {
        return await _badgeRepository.GetAll()
            .Where(badge => _badgeTrainerRepository.GetAll()
                .Any(tp => tp.TrainerId == trainerId && tp.BadgeId == badge.Id)).ToListAsync();
    }

    public async Task<List<QuestTrainerDto>> GetQuestsForTrainer(Guid trainerId)
    {
        var questList = await _questTrainerRepository.GetAll().Where(quest => quest.TrainerId == trainerId).ToListAsync();
        var outDto = _mapper.Map<List<QuestTrainerDto>>(questList);       
        return outDto;
    }

}