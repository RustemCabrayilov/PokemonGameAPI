using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;

namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(BadgeConfiguration))]
public class Badge:BaseEntity
{
    public string Name { get; set; }
    public string ThumbnailUrl { get; set; }
    public Quest Quest { get; set; }
    public Guid QuestId { get; set; }
    public List<BadgeTrainer> BadgeTrainers { get; set; }
}