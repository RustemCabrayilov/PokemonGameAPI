using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;

namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(CategoryConfiguration))]
public class Category:BaseEntity
{
    public string Name { get; set; }
    public List<Pokemon> Pokemons { get; set; }
}


