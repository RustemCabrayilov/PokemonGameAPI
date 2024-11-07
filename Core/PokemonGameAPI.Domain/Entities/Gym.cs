using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;

namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(GymConfiguration))]
public class Gym:BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Trainer Trainer { get; set; }
    public Guid TrainerId { get; set; }
    public GymLeader GymLeader { get; set; }
    public Guid GymLeaderId { get; set; }
}