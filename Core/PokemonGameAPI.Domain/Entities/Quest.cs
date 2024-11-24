using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;

namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(QuestConfiguration))]
public class Quest:BaseEntity
{
    public string Description { get; set; }
    public int TargetDamage { get; set; }
    public List<QuestTrainer> QuestTrainers { get; set; }
}