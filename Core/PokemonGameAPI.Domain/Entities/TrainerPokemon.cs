namespace PokemonGameAPI.Domain.Entities;

public class TrainerPokemon:BaseEntity
{
    public Trainer Trainer { get; set; }
    public Guid TrainerId { get; set; }
    public Pokemon Pokemon { get; set; }
    public Guid PokemonId { get; set; }
}