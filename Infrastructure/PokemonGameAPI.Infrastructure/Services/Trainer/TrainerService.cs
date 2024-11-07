using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Trainer;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.Trainer;

public class TrainerService(IGenericRepository<Domain.Entities.Trainer> _trainerRepository,
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
        var entity = await _trainerRepository.GetAsync(id);
        if(entity is null) throw new NotFoundException("Trainer not found");
        var enittyToUpdate = _mapper.Map<Domain.Entities.Trainer>(dto);
        enittyToUpdate.Id = entity.Id;
        _trainerRepository.Update(enittyToUpdate);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<TrainerResponseDto>(entity);
        return outDto;
    }

    public async Task<TrainerResponseDto> RemoveAsync(Guid id)
    {
        var entity = await _trainerRepository.GetAsync(id);
        if(entity is null) throw new NotFoundException("Trainer Not Found");
        _trainerRepository.Remove(entity);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<TrainerResponseDto>(entity);
        return outDto;
    }

    public async Task<TrainerResponseDto> GetAsync(Guid id)
    {
        var entity = await _trainerRepository.GetAsync(id);
        if(entity is null) throw new NotFoundException("Trainer Not Found");
        var outDto = _mapper.Map<TrainerResponseDto>(entity);
        return outDto;
    }

    public async Task<IList<TrainerResponseDto>> GetAllAsync()
    {
       var entities=await _trainerRepository.GetAll().ToListAsync();
       var outDtos = _mapper.Map<IList<TrainerResponseDto>>(entities);
       return outDtos;
    }
}