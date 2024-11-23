namespace PokemonGameAPI.Domain.Entities;

public class BadgeTrainer:BaseEntity
{
    public Badge Badge { get; set; }
    public Guid BadgeId { get; set; }
    public Trainer Trainer { get; set; }
    public Guid TrainerId { get; set; }
}