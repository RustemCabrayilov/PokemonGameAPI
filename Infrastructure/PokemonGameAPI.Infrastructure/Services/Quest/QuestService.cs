using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Quest;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.Quest;

public class QuestService(
    IGenericRepository<Domain.Entities.Quest> _questRepository,
    IMapper _mapper,
    IUnitOfWork _unitOfWork
    ):IQuestService
{
    public async Task<QuestResponseDto> CreateAsync(QuestRequestDto dto)
    {
        var entity=_mapper.Map<Domain.Entities.Quest>(dto);
        await _questRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var outDto = _mapper.Map<QuestResponseDto>(entity);
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
}