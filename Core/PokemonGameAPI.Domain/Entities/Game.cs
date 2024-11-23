using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;
namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(GameConfiguration))]
public class Game : BaseEntity
{
    public Guid Trainer2Id  { get; set; }
    public Trainer Trainer2 { get; set; }
    public Trainer Trainer1 { get; set; }
    public Guid Trainer1Id  { get; set; }
    public Arena? Arena { get; set; }
}