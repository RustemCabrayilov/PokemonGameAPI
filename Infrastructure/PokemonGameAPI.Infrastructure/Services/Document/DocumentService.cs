using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Document;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.Document;

public class DocumentService(IGenericRepository<Domain.Entities.Document> _documentRepository,
    IMapper _mapper,
    IUnitOfWork _unitOfWork):IDocumentService
{
    public async Task<DocumentResponseDto> CreateAsync(DocumentRequestDto dto)
    {
        var entity = _mapper.Map<Domain.Entities.Document>(dto);
        await _documentRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        var outDto = _mapper.Map<DocumentResponseDto>(entity);
        return outDto;
    }

    public async Task<DocumentResponseDto> UpdateAsync(Guid id, DocumentRequestDto dto)
    {
        var entity = await _documentRepository.GetAsync(id);
        if (entity is null) throw new NotFoundException("Document not found");
        var entityToUpdate = _mapper.Map<Domain.Entities.Document>(dto);
        entityToUpdate.Id = entity.Id;
        _documentRepository.Update(entityToUpdate);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<DocumentResponseDto>(entity);
        return outDto;
    }

    public async Task<DocumentResponseDto> RemoveAsync(Guid id)
    {
        var entity = await _documentRepository.GetAsync(id);
        if (entity is null) throw new NotFoundException("Document not found");
        _documentRepository.Remove(entity);
        _unitOfWork.SaveChanges();
        var outDto = _mapper.Map<DocumentResponseDto>(entity);
        return outDto;
    }

    public async Task<DocumentResponseDto> GetAsync(Guid id)
    {
        var entity = await _documentRepository.GetAsync(id);
        if (entity is null) throw new NotFoundException("Document not found");
        var outDto = _mapper.Map<DocumentResponseDto>(entity);
        return outDto;
    }

    public async Task<IList<DocumentResponseDto>> GetAllAsync()
    {
        var entities = await _documentRepository.GetAll().ToListAsync();
        var outDtos = _mapper.Map<IList<DocumentResponseDto>>(entities);
        return outDtos;
    }
}