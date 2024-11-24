using PokemonGameAPI.Application.Abstraction.Services.Badge;
using PokemonGameAPI.Application.Abstraction.Services.Game;
using PokemonGameAPI.Application.Abstraction.Services.Pokemon;
using PokemonGameAPI.Application.Abstraction.Services.Quest;
using PokemonGameAPI.Domain.Entities.Identity;

namespace PokemonGameAPI.Application.Abstraction.Services.Trainer;

public record TrainerResponseDto(
    Guid Id,
    int WinCount,
    int LooseCount,
    int TrainerLevel,
    AppUser User,
    List<GameResponseDto> Trainer1Games,
    List<GameResponseDto> Trainer2Games,
    List<BadgeResponseDto> Badges=null,
    List<PokemonResponseDto> Pokemons=null,
    List<QuestTrainerDto> QuestTrainers=null);