using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;
namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(GymLeaderConfiguration))]
public class GymLeader:BaseEntity
{
    public string Name { get; set; }
    public List<Gym> Gyms { get; set; }
    public List<GymLeaderPokemon> GymLeaderPokemons { get; set; }
}