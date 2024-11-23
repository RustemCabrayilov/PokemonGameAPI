using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;

namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(GymConfiguration))]
public class Gym:BaseEntity
{
    public int GymLevel { get; set; }
    public Trainer? Trainer { get; set; }
    public Guid? TrainerId { get; set; }
    public GymLeader GymLeader { get; set; }
    public Guid GymLeaderId { get; set; }
    public GymField? GymField { get; set; }
}