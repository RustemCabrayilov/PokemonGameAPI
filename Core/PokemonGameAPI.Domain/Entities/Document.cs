using PokemonGameAPI.Domain.Enums;

namespace PokemonGameAPI.Domain.Entities;

public class Document:BaseEntity
{
    public DocumentType DocumentType { get; set; }
    public string Path { get; set; }
    public string FileName { get; set; }
    public string OriginName { get; set; }
    public Guid OwnerId { get; set; }
}