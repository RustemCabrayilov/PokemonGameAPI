using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Category;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.Category;

public class CategoryService(IGenericRepository<Domain.Entities.Category> _categoryRepository,
    IMapper _mapper,
    IValidator<CategoryRequestDto> _validator,
    IUnitOfWork _unitOfWork): ICategoryService
{
    public async Task<CategoryResponseDto> CreateAsync(CategoryRequestDto dto)
    {
        var result =await _validator.ValidateAsync(dto);
        if (!result.IsValid) throw new ValidationException(result.Errors);
        var entity=_mapper.Map<Domain.Entities.Category>(dto);
       await _categoryRepository.AddAsync(entity);
       await _unitOfWork.SaveChangesAsync();
       var outDto=_mapper.Map<CategoryResponseDto>(entity);
       return outDto;
    }

    public async Task<CategoryResponseDto> UpdateAsync(Guid id, CategoryRequestDto dto)
    {
        var result =await _validator.ValidateAsync(dto);
        if (!result.IsValid) throw new ValidationException(result.Errors);
        var entity = await _categoryRepository.GetAsync(id);
        if (entity is null) throw new NotFoundException("Category not found");
        _mapper.Map(dto,entity);
        _categoryRepository.Update(entity);
        _unitOfWork.SaveChanges();
        var outDto=_mapper.Map<CategoryResponseDto>(entity);
        return outDto;
    }

    public async Task<CategoryResponseDto> RemoveAsync(Guid id)
    {
        var entity =await _categoryRepository.GetAsync(id);
        if (entity is null) throw new NotFoundException("Category not found");
        _categoryRepository.Remove(entity);
        _unitOfWork.SaveChanges();
        var outDto=_mapper.Map<CategoryResponseDto>(entity);
        return outDto;
    }

    public async Task<CategoryResponseDto> GetAsync(Guid id)
    {
      var entity =await _categoryRepository.GetAsync(id);
      if (entity is null) throw new NotFoundException("Category not found");
      var outDto = _mapper.Map<CategoryResponseDto>(entity);
      return outDto;
    }

    public async Task<IList<CategoryResponseDto>> GetAllAsync()
    {
       var entities=await _categoryRepository.GetAll().ToListAsync();
       var outDtos=_mapper.Map<IList<CategoryResponseDto>>(entities);
       return outDtos;
    }
}