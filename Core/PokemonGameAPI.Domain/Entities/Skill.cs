using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;

namespace PokemonGameAPI.Domain.Entities;

[EntityTypeConfiguration(typeof(SkillConfiguration))]
public class Skill:BaseEntity
{
    public string Name { get; set; }
    public List<Pokemon> Pokemons { get; set; }
}