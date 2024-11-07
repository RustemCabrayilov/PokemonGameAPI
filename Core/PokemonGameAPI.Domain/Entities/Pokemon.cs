using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;
using PokemonGameAPI.Domain.Enums;

namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(PokemonConfiguration))]
public class Pokemon:BaseEntity
{
    public string Name { get; set; }
    public int Level { get; set; } = 1;
    public int EvolutionLevel { get; set; }
    public int HP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public RarityEnum RarityType { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public Skill Skill { get; set; }
    public Guid SkillId { get; set; }
    public List<TrainerPokemon> TrainerPokemons { get; set; }
}