using Microsoft.AspNetCore.Http;

namespace PokemonGameAPI.Application.Abstraction.Services.Badge;

public record BadgeRequestDto(
 string Name,
 Guid QuestId,
 IFormFile File
    );