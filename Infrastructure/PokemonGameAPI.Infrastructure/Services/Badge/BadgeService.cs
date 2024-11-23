using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Badge;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Domain.Enums;
using PokemonGameAPI.Infrastructure.Exceptions;
using Document = PokemonGameAPI.Domain.Entities.Document;

namespace PokemonGameAPI.Infrastructure.Services.Badge;

public class BadgeService(
    IGenericRepository<Domain.Entities.Badge> _badgeRepository,
    IGenericRepository<Domain.Entities.Document> _documentRepository,
    IWebHostEnvironment _environment,
    IMapper _mapper,
    IUnitOfWork _unitOfWork
    ):IBadgeService
{
    public async Task<BadgeResponseDto> CreateAsync(BadgeRequestDto dto)
    {
        string fileName=Guid.NewGuid().ToString()+Path.GetExtension(dto.File.FileName);
        var localPath = _environment.ContentRootPath;
        var directoryPath = Path.Combine("Documents","Badges",fileName);
        var entity = _mapper.Map<Domain.Entities.Badge>(dto);
        entity.ThumbnailUrl = directoryPath;
        await _badgeRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var fullPath = Path.Combine(localPath,directoryPath);
        using (FileStream fs = new FileStream(fullPath, FileMode.Create))
        {
           await dto.File.CopyToAsync(fs);
        }
        var document = new Domain.Entities.Document()
        {
            OwnerId = entity.Id,
            FileName = fileName,
            OriginName = dto.File.FileName,
            Path = directoryPath,
            DocumentType = DocumentType.Badge,
        };
        await _documentRepository.AddAsync(document);
        await _unitOfWork.SaveChangesAsync();
        var outDto = _mapper.Map<BadgeResponseDto>(entity);
        return outDto;
    }

    public async Task<BadgeResponseDto> UpdateAsync(Guid id, BadgeRequestDto dto)
    {
        var entity = await _badgeRepository.GetAsync(id);
        if(entity is null) throw new NotFoundException("Badge not found");
        _mapper.Map(dto,entity);
        _badgeRepository.Update(entity);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<BadgeResponseDto>(entity);
        return outDto;
    }

    public async Task<BadgeResponseDto> RemoveAsync(Guid id)
    {
        var entity = await _badgeRepository.GetAsync(id);
        if(entity is null) throw new NotFoundException("Badge not found");
        _badgeRepository.Remove(entity);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<BadgeResponseDto>(entity);
        return outDto;
    }

    public async Task<BadgeResponseDto> GetAsync(Guid id)
    {
        var entity = await _badgeRepository.GetAsync(id);
        if(entity is null) throw new NotFoundException("Badge not found");
        var outDto = _mapper.Map<BadgeResponseDto>(entity);
        return outDto;
    }

    public async Task<IList<BadgeResponseDto>> GetAllAsync()
    {
       var entities=await _badgeRepository.GetAll().ToListAsync();
       var outDtos=_mapper.Map<IList<BadgeResponseDto>>(entities);
       return outDtos;
    }
}