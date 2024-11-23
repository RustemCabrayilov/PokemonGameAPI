namespace PokemonGameAPI.Application.Abstraction.Services.Document;

public interface IDocumentService
{
    Task<DocumentResponseDto> CreateAsync(DocumentRequestDto dto);
    Task<DocumentResponseDto> UpdateAsync(Guid id,DocumentRequestDto dto);
    Task<DocumentResponseDto> RemoveAsync(Guid id);
    Task<DocumentResponseDto> GetAsync(Guid id);
    Task<IList<DocumentResponseDto>> GetAllAsync();
}