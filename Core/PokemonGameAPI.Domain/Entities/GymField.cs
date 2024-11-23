using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;

namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(GymArenaConfiguration))]
public class GymField:BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Gym Gym { get; set; }
    public Guid GymId { get; set; }
}