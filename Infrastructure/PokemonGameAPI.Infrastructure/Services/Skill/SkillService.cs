using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Skill;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Infrastructure.Exceptions;
using ValidationException = FluentValidation.ValidationException;

namespace PokemonGameAPI.Infrastructure.Services.Skill;

public class SkillService(IGenericRepository<Domain.Entities.Skill>_skillRepository,
    IMapper _mapper,
    IValidator<SkillRequestDto> _validator,
    IUnitOfWork _unitOfWork):ISkillService
{
    public async Task<SkillResponseDto> CreateAsync(SkillRequestDto dto)
    {
        var result = await _validator.ValidateAsync(dto);
        if (!result.IsValid) throw new ValidationException(result.Errors);
        var entity = _mapper.Map<Domain.Entities.Skill>(dto);
            await _skillRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            var outDto = _mapper.Map<SkillResponseDto>(entity);
            return outDto; 
    }

    public async Task<SkillResponseDto> UpdateAsync(Guid id, SkillRequestDto dto)
    { 
        var result = await _validator.ValidateAsync(dto);
        if (!result.IsValid) throw new ValidationException(result.Errors);
        var entity = await _skillRepository.GetAsync(id);
        if(entity == null) throw new NotFoundException("Skill not found");
        var entityToUpdate = _mapper.Map<Domain.Entities.Skill>(dto);
        entityToUpdate.Id = entity.Id;
        _skillRepository.Update(entityToUpdate);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<SkillResponseDto>(entityToUpdate);
        return outDto;
    }

    public async Task<SkillResponseDto> RemoveAsync(Guid id)
    {
      var entity = await _skillRepository.GetAsync(id);
      if(entity == null) throw new NotFoundException("Skill not found");
      _skillRepository.Remove(entity);
      _unitOfWork.SaveChanges();
      var outDto = _mapper.Map<SkillResponseDto>(entity);
      return outDto;
    }

    public async Task<SkillResponseDto> GetAsync(Guid id)
    {
       var entity = await _skillRepository.GetAsync(id);
       if(entity == null) throw new NotFoundException("Skill not found");
       var outDto = _mapper.Map<SkillResponseDto>(entity);
       return outDto;
    }

    public async Task<IList<SkillResponseDto>> GetAllAsync()
    {
        var entities =_skillRepository.GetAll().ToListAsync();
        var outDtos=_mapper.Map<IList<SkillResponseDto>>(entities);
        return outDtos;
    }
}