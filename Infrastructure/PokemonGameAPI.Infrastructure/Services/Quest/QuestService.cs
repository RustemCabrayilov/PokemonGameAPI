using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Quest;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.Quest;

public class QuestService(
    IGenericRepository<Domain.Entities.Quest> _questRepository,
    IGenericRepository<Domain.Entities.Trainer> _trainerRepository,
    IGenericRepository<Domain.Entities.QuestTrainer> _questTrainerRepository,
    IGenericRepository<Domain.Entities.BattleResult> _battleResultRepository,
    IMapper _mapper,
    IUnitOfWork _unitOfWork
    ):IQuestService
{
    public async Task<QuestResponseDto> CreateAsync(QuestRequestDto dto)
    {
        var questEntity = _mapper.Map<Domain.Entities.Quest>(dto);
        await _questRepository.AddAsync(questEntity);
        await _unitOfWork.SaveChangesAsync();
        List<Guid> trainerIds = new List<Guid>();
        foreach (var trainer in await _trainerRepository.GetAll().ToListAsync())
        {
         trainerIds.Add(trainer.Id);   
        }
        foreach (var trainerId in trainerIds)
        {
            var trainerEntity = await _trainerRepository.GetAsync(trainerId);
            if (trainerEntity == null)
            {
                throw new Exception($"Trainer with ID {trainerId} not found.");
            }
            var questTrainer = new QuestTrainer
            {
                TrainerId = trainerEntity.Id,
                QuestId = questEntity.Id,
                Done = false, 
                CurrentDamage = 0 
            };
            await _questTrainerRepository.AddAsync(questTrainer);
        }
        await _unitOfWork.SaveChangesAsync();
        foreach (var trainerId in trainerIds)
        {
            var questTrainer = await _questTrainerRepository.GetAll()
                .FirstOrDefaultAsync(qt => qt.TrainerId == trainerId && qt.QuestId == questEntity.Id);
        }
        await _unitOfWork.SaveChangesAsync();
        var outDto = _mapper.Map<QuestResponseDto>(questEntity);
        return outDto;
    }


    public async Task<QuestResponseDto> UpdateAsync(Guid id, QuestRequestDto dto)
    {
        var entity = await _questRepository.GetAsync(id);
        if(entity is null) throw new NotFoundException("Quest not found");
        _mapper.Map(dto,entity);
        _questRepository.Update(entity);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<QuestResponseDto>(entity);
        return outDto;
    }

    public async Task<QuestResponseDto> RemoveAsync(Guid id)
    {
        var entity = await _questRepository.GetAsync(id);
        if(entity is null) throw new NotFoundException("Quest not found"); 
        _questRepository.Remove(entity);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<QuestResponseDto>(entity);
        return outDto;
    }

    public async Task<QuestResponseDto> GetAsync(Guid id)
    {
        var entity = await _questRepository.GetAsync(id);
        if(entity is null) throw new NotFoundException("Quest not found"); 
        var outDto = _mapper.Map<QuestResponseDto>(entity);
        return outDto;
    }

    public async Task<IList<QuestResponseDto>> GetAllAsync()
    {
        var entities = await _questRepository.GetAll().ToListAsync();
        var outDtos = _mapper.Map<IList<QuestResponseDto>>(entities);
        return outDtos;
    }

    public async Task CheckQuestAsync(Guid trainerId)
    {
        var winnerResults = await _battleResultRepository.GetAll().Where(x=>x.WinnerId==trainerId).ToListAsync();
        var looserResults = await _battleResultRepository.GetAll().Where(x=>x.LooserId==trainerId).ToListAsync();
        var totalDamage = 0;
        foreach (var winnerResult in winnerResults)
        {
            totalDamage += winnerResult.WinnerDamages;
        }
        foreach (var looserResult in looserResults)
        {
            totalDamage += looserResult.LooserDamages;
        }
        var quests = await _questRepository.GetAll()
            .Where(quest => _questTrainerRepository.GetAll()
                .Any(tp => tp.TrainerId == trainerId && tp.QuestId == quest.Id)).ToListAsync();
        foreach (var quest in quests)
        {
            var questTrainer=await _questTrainerRepository.GetAll().FirstOrDefaultAsync(x=>x.QuestId==quest.Id);
            if(questTrainer is null) throw new NotFoundException("Quest not found");
            if (questTrainer.CurrentDamage >= quest.TargetDamage)
            {
                questTrainer.Done = true;
                questTrainer.CurrentDamage = totalDamage;
                _questTrainerRepository.Update(questTrainer);
            }
        }
        await _unitOfWork.SaveChangesAsync();
    }
}