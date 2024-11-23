using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;

namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(QuestConfiguration))]
public class Quest:BaseEntity
{
    public int KillCount { get; set; }
    public int CurrentKillCount { get; set; } 
    public bool Done { get; set; }
    public List<QuestTrainer> QuestTrainers { get; set; }
}