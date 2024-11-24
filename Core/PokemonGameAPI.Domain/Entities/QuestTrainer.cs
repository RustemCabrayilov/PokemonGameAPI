namespace PokemonGameAPI.Domain.Entities;

public class QuestTrainer:BaseEntity
{
    public Quest Quest { get; set; }
    public Guid QuestId { get; set; }
    public Trainer Trainer { get; set; }
    public Guid TrainerId { get; set; }
    public bool Done { get; set; }
    public int CurrentDamage { get; set; } 
}