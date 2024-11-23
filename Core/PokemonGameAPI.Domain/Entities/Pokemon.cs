using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;
using PokemonGameAPI.Domain.Enums;

namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(PokemonConfiguration))]
public class Pokemon:BaseEntity
{
    public string Name { get; set; }
    public int PokemonLevel { get; set; } = 1;
    public int EvolutionLevel { get; set; }
    public int HP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public AttitudeType AttitudeType { get; set; } = Enums.AttitudeType.Stabil;
    public bool ReadyForBattle { get; set; } 
    public RarityEnum RarityType { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public Skill Skill { get; set; }
    public Guid SkillId { get; set; }
    
    public List<PokemonTrainer> TrainerPokemons { get; set; }
    public List<GymLeaderPokemon> GymLeaderPokemon { get; set; }
 
}