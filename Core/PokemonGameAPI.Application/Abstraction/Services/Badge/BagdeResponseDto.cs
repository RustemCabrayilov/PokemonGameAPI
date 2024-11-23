namespace PokemonGameAPI.Application.Abstraction.Services.Badge;

public record BadgeResponseDto(
    Guid Id,
    string Name,
    string ThumbnailUrl,
    Domain.Entities.Quest Quest);