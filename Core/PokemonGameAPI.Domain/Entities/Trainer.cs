using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;
using PokemonGameAPI.Domain.Entities.Identity;
namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(TrainerConfiguration))]
public class Trainer:BaseEntity
{
    public int WinCount { get; set; }
    public int LooseCount { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public List<TrainerPokemon> TrainerPokemons { get; set; }
    public List<Game> Games { get; set; }
}